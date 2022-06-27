using JianXiEditor.Model;
using System.Threading.Tasks;
using System.Windows;
using System;
using JianXiEditor.Service.Initialize;
using JianXiEditor.Service;
using JianXiEditor.View;
using JianXiEditor.Config;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit;
using System.Windows.Controls;
using HandyControl.Tools;
using System.Globalization;
using HandyControl.Tools.Command;
using System.Windows.Input;
using System.IO;
using JianXiEditor.Utility.DrapDrop;
using JianXiEditor.Utility;
using JianXiEditor.Service.EditorPlus;
using System.Diagnostics;

namespace JianXiEditor.ViewModel
{
    class MainViewModel : BindablePropertyBase, IFilesDropped
    {
        private MainWindow win;
        private TextEditor textEditor;

        /// <summary>
        /// 程序初始化
        /// </summary>
        public RelayCommand WindowLoadCommand => new RelayCommand(new Action<object>(obj =>
        {
            //配置初始化开始
            Task.Run(() =>
            {
                Initialize_Config config = new Initialize_Config();
                Logger.LoggerHandlerManager.AddHandler(new FileLoggerHandler($"Log_{DateTime.Now.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo)}.log", Config_Temp.AppDataPath_Log));//开启日志记录功能
                new Initialize_ConfigFile().Initialize_ConfigFileMain();//初始化配置文件
                config.Initialize_Config_Theme();//主题单独加载，用户体验会更好
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                    new Action(() =>
                    {
                        win = obj as MainWindow;
                        textEditor = win.CodeEditor;
                        config.Initialize_ConfigMain(Editor, Setting);//初始化程序配置
                        config.Initialize_Config_EditorLocation(textEditor);//加载编辑器位置
                        DataObject.AddSettingDataHandler(textEditor, EditorTextFormat.onTextViewSettingDataHandler);//取消复制带格式的文本
                        textEditor.TextArea.TextView.LineSpacing = Config_Editor.Config_LineHeight;//加载文本控件行高
                        new Initialize_App().InitBackUpOrAssociated(Main, Editor);//初始化关联文件和备份文件的加载
                        new HistoryFile().LoadHistoryList(win.HistoryListMenu);//加载文件到右键菜单
                        HighlightSameWord.HiSelect(textEditor);//高亮相同单词
                        new AutoSaveFile().Initialize(this);//自动保存文件
                    }));
            });
        }));

        #region 初始化绑定
        private MainModel main;
        public MainModel Main
        {
            get
            {
                if (main == null)//如果不是空的，就说明已经实例化了
                {
                    main = new MainModel();
                }
                return main;
            }
            set => Set(ref main, value);
        }
        private EditorModel editor;
        public EditorModel Editor
        {
            get
            {
                if (editor == null)//如果不是空的，就说明已经实例化了
                {
                    editor = new EditorModel();
                }
                return editor;
            }
            set => Set(ref editor, value);
        }
        private SettingModel setting;
        public SettingModel Setting
        {
            get
            {
                if (setting == null)//如果不是空的，就说明已经实例化了
                {
                    setting = new SettingModel();
                }
                return setting;
            }
            set => Set(ref setting, value);
        }
        #endregion

        #region 命令

        #region ControlBar
        /// <summary>
        /// 关闭命令
        /// </summary>
        public RelayCommand AppExitCommand => new RelayCommand(obj =>
        {
            using (FileManager fileOpeare = new FileManager())
            {
                if (Config_App.Config_FileCache == true)//如果打开了关闭时备份文件功能
                {
                    fileOpeare.ExitFileBackUp(Main);
                    Application.Current.Shutdown();
                }
                else//没有打开就要提示是否保存
                {
                    if (fileOpeare.ExitFileSave(Main, Editor) == true)
                    {
                        Application.Current.Shutdown();
                    }
                }
            }
        });
        /// <summary>
        /// 最小化命令
        /// </summary>
        public RelayCommand WindowMinCommand => new RelayCommand(obj =>
        {
            if (win != null)
            {
                win.WindowState = WindowState.Minimized;
            }
        });
        /// <summary>
        /// 最大化/恢复正常命令
        /// </summary>
        public RelayCommand WindowMaxOrNormalCommand => new RelayCommand(obj =>
        {
            if (win != null)
            {
                switch (win.WindowState)
                {
                    case WindowState.Normal:
                        win.WindowState = WindowState.Maximized;
                        break;
                    case WindowState.Maximized:
                        win.WindowState = WindowState.Normal;
                        break;
                    default:
                        break;
                }
            }
        });
        /// <summary>
        /// 最大化/恢复正常命令（双击）
        /// </summary>
        public RelayCommand WindowDoubleMaxOrNormalCommand => new RelayCommand(new Action<object>(obj =>
        {
            MouseButtonEventArgs e = obj as MouseButtonEventArgs;
            if (e != null)
            {
                if (e.ClickCount == 2)
                {
                    switch (win.WindowState)
                    {
                        case WindowState.Normal:
                            win.WindowState = WindowState.Maximized;
                            break;
                        case WindowState.Maximized:
                            win.WindowState = WindowState.Normal;
                            break;
                        default:
                            break;
                    }
                }
            }
        }));
        #endregion

        #region NavBar
        /// <summary>
        /// 文件命令
        /// </summary>
        public RelayCommand FileCommand => new RelayCommand(new Action<object>(obj =>
        {
            try
            {
                using (FileManager fileOpeare = new FileManager())
                {
                    switch (obj as string)
                    {
                        case "New":
                            fileOpeare.NewFile(Main, Editor);
                            break;
                        case "Open":
                            fileOpeare.OpenFile(Main, Editor);
                            break;
                        case "Save":
                            if (fileOpeare.SaveFile(Main, Editor) == true)
                            {
                                HandyControl.Controls.Growl.Success("保存成功！");
                            }
                            break;
                        case "SaveAs":
                            if (fileOpeare.SaveAsFile(Main, Editor) == true)
                            {
                                HandyControl.Controls.Growl.Success("保存成功！");
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                //记录错误日志
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("出现未知错误，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        }));
        /// <summary>
        /// 查找和替换窗口
        /// </summary>
        public RelayCommand FindReplaceCommand => new RelayCommand(obj =>
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is FindReplaceWindow)
                {
                    item.Close();
                    return;
                }
            }
            FindReplaceWindow findReplaceWindow = new FindReplaceWindow(textEditor.SelectedText)
            {
                Owner = win
            };
            findReplaceWindow.Show();
        });
        #endregion

        #region TextEditor
        /// <summary>
        /// 注释
        /// </summary>
        public RelayCommand CodeAnnotationCommand => new RelayCommand(obj =>
        {
            try
            {
                if (textEditor != null)
                {
                    new CommentHandle().InsertOrRemoveComment(Main, textEditor);
                }
            }
            catch (Exception ex)
            {
                //记录错误日志
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("出现未知错误，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        });
        /// <summary>
        /// 当输入内容时
        /// </summary>
        public RelayCommand PreviewTextInputCommand => new RelayCommand(new Action<object>(obj =>
        {
            try
            {
                TextCompositionEventArgs e = obj as TextCompositionEventArgs;
                if (e != null)
                {
                    new CommentHandle().CSharpThereComment(Main, textEditor, e);
                    new TagHandle().TagAutoClose(Setting, textEditor, e);
                }
            }
            catch (Exception ex)
            {
                //记录错误日志
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("发生未知错误，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        }));
        /// <summary>
        /// 文件发生改变时
        /// </summary>
        public RelayCommand TextChangedCommand => new RelayCommand(obj =>
        {
            Task.Run(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
                    new Action(() =>
                    {
                        if (!File.Exists(Main.FilePath))//没有打开过文件
                        {
                            Main.SaveStatus = "未保存";//没有保存过
                        }
                        else//打开过文件
                        {
                            if (Setting.RealDisplayState == true)
                            {
                                if (Config_Temp.FileContent == Main.Document.Text)
                                {
                                    Main.SaveStatus = "已保存";//保存过
                                }
                                else
                                {
                                    Main.SaveStatus = "未保存";//没有保存过
                                }
                            }
                            else
                            {
                                if (Config_Temp.FileContent.Length == Main.Document.TextLength)
                                {
                                    Main.SaveStatus = "已保存";//保存过
                                }
                                else
                                {
                                    Main.SaveStatus = "未保存";//没有保存过
                                }
                            }
                        }
                    }));
            });
        });
        /// <summary>
        /// 选中文字
        /// </summary>
        public RelayCommand SelectionCommand => new RelayCommand(obj =>
        {
            if (textEditor != null)
            {
                Main.SelectTextLength = textEditor.SelectionLength;
            }
        });
        /// <summary>
        /// 双击文本框
        /// </summary>
        public RelayCommand MouseDoubleCommand => new RelayCommand(obj =>
        {
            try
            {
                if (textEditor != null)
                {
                    if (textEditor.Document.TextLength > 0 && Setting.QuickCopyLine == true)
                    {
                        new LineHandle().CopyLineContent(textEditor);//复制当前行
                        HandyControl.Controls.Growl.Success("复制成功！");
                    }
                }
            }
            catch (Exception ex)
            {
                //记录错误日志
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("复制当前行的内容时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        });
        /// <summary>
        /// 快速向左/右选择/移动文字
        /// </summary>
        public RelayCommand QuickTextCommand => new RelayCommand(new Action<object>(obj =>
        {
            try
            {
                switch (obj as string)
                {
                    case "Left":
                        if (Setting.FastMoveText == true)
                        {
                            new TextHandle().MoveLeftWord(textEditor);
                        }
                        else
                        {
                            new TextHandle().SelectLeftWord(textEditor);
                        }
                        break;
                    case "Right":
                        if (Setting.FastMoveText == true)
                        {
                            new TextHandle().MoveRightWord(textEditor);
                        }
                        else
                        {
                            new TextHandle().SelectRightWord(textEditor);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                //记录错误日志
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("向左选择/移动文字时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        }));
        /// <summary>
        /// 快速向上/下选择/移动行
        /// </summary>
        public RelayCommand QuickLineCommand => new RelayCommand(new Action<object>(obj =>
        {
            try
            {
                switch (obj as string)
                {
                    case "Up":

                        if (Setting.FastMoveLine == true)
                        {
                            new LineHandle().MoveUpLine(textEditor, Main);
                        }
                        else
                        {
                            new LineHandle().SelectUpLine(textEditor);
                        }
                        break;
                    case "Down":

                        if (Setting.FastMoveLine == true)
                        {
                            new LineHandle().MoveDownLine(textEditor, Main);
                        }
                        else
                        {
                            new LineHandle().SelectDownLine(textEditor);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                //记录错误日志
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("向下选择/移动行时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        }));
        /// <summary>
        /// 将当前行插入到下一行
        /// </summary>
        public RelayCommand CurrentLineInsertNextLineCommand => new RelayCommand(new Action<object>(obj =>
        {
            try
            {
                new LineHandle().CurrentLineInsertNextLine(textEditor, Main);
            }
            catch (Exception ex)
            {
                //记录错误日志
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("将当前行插入到下一行时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        }));
        /// <summary>
        /// 复制整个文档
        /// </summary>
        public RelayCommand CopyAllContentCommand => new RelayCommand(obj =>
        {
            if (textEditor.Text != "" && textEditor.Text != " ")
            {
                Clipboard.SetText(textEditor.Text);
            }
        });
        /// <summary>
        /// 剪切并删除行
        /// </summary>
        public RelayCommand CutLineCommand => new RelayCommand(obj =>
        {
            try
            {
                new LineHandle().CutLine(textEditor, Main);
            }
            catch (Exception ex)
            {
                //记录错误日志
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("剪切行时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        });
        /// <summary>
        /// 跳转到下一个相同的内容
        /// </summary>
        public RelayCommand ToNextContentCommand => new RelayCommand(obj =>
        {
            try
            {
                FindAndReplace.FindNext(textEditor.SelectedText, false, false, false, false, false);
            }
            catch (Exception ex)
            {
                //记录错误日志
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("跳转到下一个内容时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        });
        /// <summary>
        /// 移除所有换行符
        /// </summary>
        public RelayCommand RemoveAllLineBreakCommand => new RelayCommand(obj =>
        {
            try
            {
                string lineBreak = null;
                switch (main.FileLineBreak)
                {
                    case "CRLF":
                        lineBreak = "\r\n";
                        break;
                    case "LF":
                        lineBreak = "\n";
                        break;
                    case "CR":
                        lineBreak = "\r";
                        break;
                }
                string oldText = textEditor.Text;
                textEditor.Text = oldText.Replace(lineBreak, "");
            }
            catch (Exception ex)
            {
                //记录错误日志
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("移除换行符时出现错误，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        });
        /// <summary>
        /// 按下Alt+C会复制当前行
        /// </summary>
        public RelayCommand NoSelectedCopyCommand => new RelayCommand(obj =>
        {
            try
            {
                if (textEditor.SelectionLength == 0)
                {
                    int currentLineNumber = textEditor.TextArea.Caret.Line;//获取当前行位置
                    int currentLineOffset = textEditor.Document.GetLineByNumber(currentLineNumber).Offset;//获取当前行的起始位置
                    int currentLineLength = textEditor.Document.GetLineByNumber(currentLineNumber).Length;//获取当前行的长度
                    Clipboard.SetText(textEditor.Document.GetText(currentLineOffset, currentLineLength));
                }
            }
            catch (Exception ex)
            {
                //记录错误日志
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("快速复制时出现错误，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        });
        /// <summary>
        /// 拖拽打开文件
        /// </summary>
        /// <param name="files"></param>
        public void OnFilesDropped(string[] files)
        {
            try
            {
                if (files.Length <= 0)
                {
                    return;
                }
                using (FileManager fileOpeare = new FileManager())
                {
                    fileOpeare.DropDrapOpenFile(Main, Editor, files[0]);
                }
            }
            catch (Exception ex)
            {
                //记录错误日志
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("拖拽打开文件时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        }
        #endregion

        #region StatusBar
        /// <summary>
        /// 打开详细字数统计窗口
        /// </summary>
        public RelayCommand TextCountWindowCommand => new RelayCommand(new Action<object>(obj =>
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is TextCountWindow)
                {
                    item.Close();
                    return;
                }
            }
            bool isSelect = bool.Parse(obj as string);
            TextCountWindow textCountWindow = new TextCountWindow(isSelect)
            {
                Owner = win
            };
            textCountWindow.Show();
        }));
        /// <summary>
        /// 打开更改换行符格式窗口
        /// </summary>
        public RelayCommand LineBreakCommand => new RelayCommand(obj =>
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is LineBreakWindow)
                {
                    item.Close();
                    return;
                }
            }
            LineBreakWindow lineBreakWindow = new LineBreakWindow(Main.FileLineBreak)
            {
                Owner = win
            };
            lineBreakWindow.Show();
        });
        /// <summary>
        /// 打开更改编码格式窗口
        /// </summary>
        public RelayCommand EncodingCommand => new RelayCommand(obj =>
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is EncodingWindow)
                {
                    item.Close();
                    return;
                }
            }
            EncodingWindow encodingWindow = new EncodingWindow(Main.FileEncoding)
            {
                Owner = win
            };
            encodingWindow.Show();
        });
        /// <summary>
        /// 打开语法高亮语言选择窗口
        /// </summary>
        public RelayCommand OpenSyntaxWindowCommand => new RelayCommand(obj =>
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is SyntaxHighlightingWindow)
                {
                    item.Close();
                    return;
                }
            }
            SyntaxHighlightingWindow syntaxHighlightingWindow = new SyntaxHighlightingWindow(Main.FileSyntaxHighlighting)
            {
                Owner = win
            };
            syntaxHighlightingWindow.Show();
        });
        /// <summary>
        /// 打开跳转行窗口
        /// </summary>
        public RelayCommand ToLineCommand => new RelayCommand(obj =>
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is ToLineWindow)
                {
                    item.Close();
                    return;
                }
            }
            ToLineWindow toLineWindow = new ToLineWindow
            {
                Owner = win
            };
            toLineWindow.Show();
        });
        /// <summary>
        /// 打开重命名窗口
        /// </summary>
        public RelayCommand FilePathCommand => new RelayCommand(obj =>
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is FilePathWindow)
                {
                    item.Close();
                    return;
                }
            }
            FilePathWindow filePathWindow = new FilePathWindow(Main.FilePath)
            {
                Owner = win
            };
            filePathWindow.Show();
        });
        /// <summary>
        /// 打开设置窗口
        /// </summary>
        public RelayCommand OpenSettingWindowCommand => new RelayCommand(obj =>
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is SettingWindow)
                {
                    item.Close();
                    return;
                }
            }
            SettingWindow settingWindow = new SettingWindow
            {
                Owner = win
            };
            settingWindow.Show();
        });
        /// <summary>
        /// 打开帮助窗口
        /// </summary>
        public RelayCommand HelpCommand => new RelayCommand(obj =>
        {
            Process.Start(new ProcessStartInfo("https://meixiapp.com/pages/document_JianXiEditor.html") { UseShellExecute = true });//调用默认浏览器
        });
        /// <summary>
        /// 打开快捷键窗口
        /// </summary>
        public RelayCommand ShortcutKeyWindowCommand => new RelayCommand(obj =>
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is ShortcutKeyWindow)
                {
                    item.Close();
                    return;
                }
            }
            ShortcutKeyWindow shortcutKeyWindow = new ShortcutKeyWindow
            {
                Owner = win
            };
            shortcutKeyWindow.Show();
        });
        /// <summary>
        /// 打开关于窗口
        /// </summary>
        public RelayCommand AboutWindowCommand => new RelayCommand(obj =>
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is AboutWindow)
                {
                    item.Close();
                    return;
                }
            }
            AboutWindow aboutWindow = new AboutWindow
            {
                Owner = win
            };
            aboutWindow.Show();
        });
        #endregion

        #region Other
        /// <summary>
        /// 网络搜索
        /// </summary>
        public RelayCommand WebSearchCommand => new RelayCommand(obj =>
       {
           if (textEditor != null)
           {
               Process.Start(new ProcessStartInfo("https://www.baidu.com/s?wd=" + textEditor.SelectedText) { UseShellExecute = true });//调用默认浏览器
           }
       });
        /// <summary>
        /// 打印
        /// </summary>
        public RelayCommand PrintCommand => new RelayCommand(obj =>
        {
            try
            {
                PrintDialog pd = new PrintDialog();
                if (pd.ShowDialog() == true)
                {
                    if (textEditor != null)
                    {
                        pd.PrintVisual(textEditor, Main.Document.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                //记录错误日志
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("调用打印机时出现异常，已将异常原因记录到如下目录：" + Config_Temp.AppDataPath_Log);
            }
        });
        #endregion

        #endregion
    }
}