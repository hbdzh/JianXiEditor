using ICSharpCode.AvalonEdit;
using JianXiEditor.Config;
using JianXiEditor.Model;
using JianXiEditor.View;
using System;
using System.IO;
using System.Windows;

namespace JianXiEditor.Service.Initialize
{
    /// <summary>
    /// 根据配置文件初始化程序
    /// </summary>
    class Initialize_Config
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="editor"></param>
        public void Initialize_ConfigMain(EditorModel editor, SettingModel setting)
        {
            Initialize_Config_App(setting);
            Initialize_Config_Editor(editor);
            Initialize_Config_Update();
        }
        /// <summary>
        /// 初始化编辑器配置
        /// </summary>
        /// <param name="editor"></param>
        private void Initialize_Config_Editor(EditorModel editor)
        {
            try
            {
                editor.SelelctFontSize = Config_Editor.Config_FontSize;
                editor.WordWrap = Config_Editor.Config_WordWarp;
                editor.ShowLineNumbers = Config_Editor.Config_ShowLineNumbers;
                editor.TextOptions.EnableVirtualSpace = Config_Editor.Config_VirtualSpace;
                editor.TextOptions.HighlightCurrentLine = Config_Editor.Config_HighlightCurrentLine;
                editor.TextOptions.ConvertTabsToSpaces = Config_Editor.Config_ConvertTabsToSpaces;
                editor.TextOptions.EnableEmailHyperlinks = Config_Editor.Config_EnableEmailHyperlinks;
                editor.TextOptions.EnableHyperlinks = Config_Editor.Config_EnableHyperlinks;
                editor.TextOptions.ShowEndOfLine = Config_Editor.Config_ShowEndOfLine;
                editor.TextOptions.ShowSpaces = Config_Editor.Config_ShowSpaces;
                editor.TextOptions.AllowScrollBelowDocument = Config_Editor.Config_AllowScrollBelowDocument;
                editor.TextOptions.CutCopyWholeLine = Config_Editor.Config_CutCopyWholeLine;
                editor.TextOptions.ShowTabs = Config_Editor.Config_ShowTabs;
                editor.TextOptions.IndentationSize = Config_Editor.Config_IndentationSize;
                editor.TextOptions.HideCursorWhileTyping = Config_Editor.Config_HideCursorWhileTyping;
                editor.TextOptions.ColumnRulerPosition = Config_Editor.Config_ColumnRulerPosition;
                editor.TextOptions.ShowColumnRuler = Config_Editor.Config_ShowColumnRuler;
            }
            catch (Exception ex)
            {
                HandyControl.Tools.Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("初始化编辑器时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        }
        /// <summary>
        /// 初始化程序配置
        /// </summary>
        private void Initialize_Config_App(SettingModel setting)
        {
            try
            {
                setting.SelectAppTheme = Config_App.Config_UseTheme;
                setting.SelectEditorLocation = Config_App.Config_EditorLocation;
                setting.FastMoveText = Config_App.Config_FastMoveText;
                setting.FastMoveLine = Config_App.Config_FastMoveLine;
                setting.QuickCopyLine = Config_App.Config_QuickCopyLine;
                setting.AutoUpdate = Config_App.Config_AutoUpdate;
                setting.RealDisplayState = Config_App.Config_RealDisplayState;
                setting.FileCache = Config_App.Config_FileCache;
                setting.SelectLineHeight = Config_Editor.Config_LineHeight;
                setting.HighlightSameWord = Config_App.Config_HighlightSameWord;
                setting.SelectFont = Config_App.Config_FontFamily;
                setting.TagAutoClose = Config_App.Config_TagAutoClose;
                setting.AutoSaveFile = Config_App.Config_AutoSaveFile;

            }
            catch (Exception ex)
            {
                HandyControl.Tools.Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("初始化程序配置时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        }
        /// <summary>
        /// 初始化编辑器位置
        /// </summary>
        /// <param name="window"></param>
        /// <param name="textEditor"></param>
        public void Initialize_Config_EditorLocation(TextEditor textEditor)
        {
            if (Config_App.Config_EditorLocation == "Center")
            {
                double padding = (textEditor.ActualWidth - textEditor.ActualWidth / 2) / 3;
                textEditor.Padding = new Thickness(padding, 5, padding, 0);
            }
            else
            {
                textEditor.Padding = new Thickness(0, 5, 10, 0);
            }
        }
        /// <summary>
        /// 初始化主题
        /// </summary>
        public void Initialize_Config_Theme()
        {
            try
            {
                ResourceDictionary resource = new ResourceDictionary
                {
                    Source = new Uri("pack://application:,,,/简兮编辑器;component/Resource/Theme/" + Config_App.Config_UseTheme + ".xaml")
                };
                Application.Current.Resources.MergedDictionaries[2] = resource;//因为目前第二个是主题，如果以后改变了App.xaml中的加载顺序，这里务必要改
            }
            catch (Exception ex)
            {
                HandyControl.Tools.Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("加载主题时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        }
        /// <summary>
        /// 检查更新
        /// </summary>
        private void Initialize_Config_Update()
        {
            //如果已经存在安装文件，就先删掉它，防止旧版本的安装包一直存在
            if (File.Exists(Config_Temp.AppDataPath_Update + Config_Temp.AppSetupName))
            {
                File.Delete(Config_Temp.AppDataPath_Update + Config_Temp.AppSetupName);
            }
            if (Config_App.Config_AutoUpdate == true && HandyControl.Tools.ApplicationHelper.IsConnectedToInternet() == true)//检查是否开启自动更新
            {
                if (new UpdateManager().GetUpdateFile() == true)//检查更新文件是否下载成功（这里不建议将该行代码与外部if合并）
                {
                    if (int.Parse(UpdateManager.LocalVersion.Replace(".", "")) < int.Parse(UpdateManager.LatestVersion.Replace(".", "")))//比对两个版本，如果不同就显示更新页面（本地版本小于网络版本，才会更新）
                    {
                        UpdateWindow update = new UpdateWindow
                        {
                            Owner = Application.Current.MainWindow
                        };
                        update.ShowDialog();
                    }
                    else
                    {
                        if (File.Exists($"{Config_Temp.AppDataPath_Update}Update.sin"))
                        {
                            File.Delete($"{Config_Temp.AppDataPath_Update}Update.sin");
                        }
                    }
                }
            }
        }
    }
}
