using HandyControl.Tools;
using HandyControl.Tools.Command;
using ICSharpCode.AvalonEdit;
using JianXiEditor.Config;
using JianXiEditor.Service;
using JianXiEditor.Model;
using JianXiEditor.View;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using MessageBox = HandyControl.Controls.MessageBox;
using System.Threading.Tasks;

namespace JianXiEditor.ViewModel
{
    class FindReplaceViewModel : BindablePropertyBase
    {
        private FindReplaceModel findReplace;
        public FindReplaceModel FindReplace
        {
            get
            {
                if (findReplace == null)//如果不是空的，就说明已经实例化了
                {
                    findReplace = new FindReplaceModel();
                }
                return findReplace;
            }
            set
            {
                findReplace = value;
                RaisePropertyChanged(nameof(FindReplace));
            }
        }
        private List<FindFilesModel> findFilesList;
        public List<FindFilesModel> FindFilesList
        {
            get
            {
                if (findFilesList == null)//如果不是空的，就说明已经实例化了
                {
                    findFilesList = new List<FindFilesModel>();
                }
                return findFilesList;
            }
            set => Set(ref findFilesList, value);
        }
        /// <summary>
        /// 查找
        /// </summary>
        public RelayCommand FindCommand => new RelayCommand(obj =>
        {
            if (!FindAndReplace.FindNext(FindReplace.FindContent, findReplace.SearchUp, findReplace.MatchCase, findReplace.RegExp, findReplace.WildCards, findReplace.MatchWholeWord))
            {
                HandyControl.Controls.Growl.Success("没有找到相关内容！");
            }
        });
        /// <summary>
        /// 替换
        /// </summary>
        public RelayCommand ReplaceCommand => new RelayCommand(obj =>
        {
            Regex regex = FindAndReplace.GetRegEx(FindReplace.FindContent, findReplace.SearchUp, findReplace.MatchCase, findReplace.RegExp, findReplace.WildCards, findReplace.MatchWholeWord, false);
            TextEditor textEditor = (Application.Current.MainWindow as MainWindow).CodeEditor;
            string input = textEditor.Document.Text.Substring(textEditor.SelectionStart, textEditor.SelectionLength);
            Match match = regex.Match(input);
            bool replaced = false;
            if (match.Success && match.Index == 0 && match.Length == input.Length)
            {
                textEditor.Document.Replace(textEditor.SelectionStart, textEditor.SelectionLength, FindReplace.ReplaceContent);
                replaced = true;
            }
            if (!FindAndReplace.FindNext(FindReplace.FindContent, findReplace.SearchUp, findReplace.MatchCase, findReplace.RegExp, findReplace.WildCards, findReplace.MatchWholeWord) && !replaced)
            {
                HandyControl.Controls.Growl.Success("没有找到相关内容！");
            }
        });
        /// <summary>
        /// 替换全部
        /// </summary>
        public RelayCommand ReplaceAllCommand => new RelayCommand(obj =>
        {
            int offset = 0;
            TextEditor textEditor = (Application.Current.MainWindow as MainWindow).CodeEditor;
            textEditor.BeginChange();
            Regex regex = FindAndReplace.GetRegEx(FindReplace.FindContent, findReplace.SearchUp, findReplace.MatchCase, findReplace.RegExp, findReplace.WildCards, findReplace.MatchWholeWord, true);
            foreach (Match match in regex.Matches(textEditor.Document.Text))
            {
                textEditor.Document.Replace(offset + match.Index, match.Length, FindReplace.ReplaceContent);
                offset += FindReplace.ReplaceContent.Length - match.Length;
            }
            textEditor.EndChange();
        });
        /// <summary>
        /// 切换是否开启在文件夹中查找功能
        /// </summary>
        public RelayCommand ChangeFolderFindCommand => new RelayCommand(new Action<object>(obj =>
        {
            WrapPanel panel = obj as WrapPanel;
            if (FindReplace.InFolderFind == true)
            {
                panel.Visibility = Visibility.Visible;
            }
            else
            {
                panel.Visibility = Visibility.Collapsed;
            }
        }));
        /// <summary>
        /// 在文件夹中查找
        /// </summary>
        public RelayCommand FolderFindCommand => new RelayCommand(obj =>
        {
            FindFilesList.Clear();//每次添加前先清空掉之前的内容
            Task.Run(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                    new Action(() =>
                    {
                        try
                        {
                            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                            MainViewModel main = mainWindow.DataContext as MainViewModel;
                            FindAndReplace find = new FindAndReplace();
                            string[] files = find.GetFiles(main.Main.FilePath, FindReplace.FileType);
                            Dictionary<int, string> list;
                            for (int i = 0; i < files.Length; i++)
                            {
                                list = find.FindFileContent(files[i], FindReplace.FindContent);
                                if (list.Count >= 1)
                                {
                                    foreach (KeyValuePair<int, string> a in list)
                                    {
                                        FindFilesList.Add(new FindFilesModel { FilePath = files[i], FileContent = a.Value, FileLine = a.Key.ToString() });
                                    }
                                }
                            }
                            FindFilesWindow findFilesWindow = new FindFilesWindow
                            {
                                Owner = mainWindow
                            };
                            findFilesWindow.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            //记录错误日志
                            Logger.Log(ex);
                            HandyControl.Controls.Growl.Warning("查找时出现异常，已将异常原因记录到如下目录：" + Config_Temp.AppDataPath_Log);
                        }
                    }));
            });
        });
        /// <summary>
        /// 在文件夹中替换
        /// </summary>
        public RelayCommand FolderReplaceCommand => new RelayCommand(obj =>
        {
            Task.Run(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                    new Action(() =>
                    {
                        try
                        {
                            switch (MessageBox.Show("请您再进行该操作之前，将文件进行备份，以免造成不可挽回的损失！", "是否替换文件中的内容？", MessageBoxButton.YesNo, MessageBoxImage.Information))
                            {
                                case MessageBoxResult.Yes:
                                    MainViewModel main = Application.Current.MainWindow.DataContext as MainViewModel;
                                    FindAndReplace find = new FindAndReplace();
                                    string[] files = find.GetFiles(main.Main.FilePath, FindReplace.FileType);
                                    for (int i = 0; i < files.Length; i++)
                                    {
                                        find.ReplaceFileContent(files[i], FindReplace.FindContent, FindReplace.ReplaceContent);
                                    }
                                    HandyControl.Controls.Growl.Success("替换成功！");
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            //记录错误日志
                            Logger.Log(ex);
                            HandyControl.Controls.Growl.Warning("替换时出现异常，已将异常原因记录到如下目录：" + Config_Temp.AppDataPath_Log);
                        }
                    }));
            });
        });
    }
}
