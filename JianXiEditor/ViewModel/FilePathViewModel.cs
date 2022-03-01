using HandyControl.Tools;
using HandyControl.Tools.Command;
using ICSharpCode.AvalonEdit.Highlighting;
using JianXiEditor.Config;
using JianXiEditor.Model;
using JianXiEditor.Utility;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace JianXiEditor.ViewModel
{
    class FilePathViewModel : BindablePropertyBase
    {
        public static string TempFileName { get; set; } = null;

        private FilePathModel filePath;
        public FilePathModel FilePath
        {
            get
            {
                if (filePath == null)//如果不是空的，就说明已经实例化了
                {
                    filePath = new FilePathModel();
                }
                return filePath;
            }
            set => Set(ref filePath, value);
        }
        /// <summary>
        /// 关闭窗口时重命名文件
        /// </summary>
        public RelayCommand ClosingCommand => new RelayCommand(obj =>
        {
            if (TempFileName != FilePath.FileName)//如果不一样，说明改过文件名了；如果一样说明没改过，就不再执行重命名操作
            {
                try
                {
                    MainViewModel main = Application.Current.MainWindow.DataContext as MainViewModel;
                    if (!File.Exists(main.Main.FilePath))
                    {
                        string newFilePath = main.Main.FilePath.Replace(Path.GetFileName(main.Main.FilePath), FilePath.FileName);
                        string oldFilePath = main.Main.FilePath;
                        main.Main.FilePath = newFilePath;
                        main.Main.FileSyntaxHighlighting = FileExtension.ToName(Path.GetExtension(newFilePath));
                        main.Editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(FileExtension.ToExtension(main.Main.FileSyntaxHighlighting));
                        File.Move(oldFilePath, newFilePath);
                    }
                    else
                    {
                        main.Main.FilePath = FilePath.FileName;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    HandyControl.Controls.Growl.Warning("重命名文件时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
                }
            }
        });
        /// <summary>
        /// 打开所在文件夹
        /// </summary>
        public RelayCommand OpenFileFolderCommand => new RelayCommand(obj =>
        {
            MainViewModel main = (MainViewModel)Application.Current.MainWindow.DataContext;
            if (File.Exists(main.Main.FilePath))
            {
                Process.Start("explorer.exe", Path.GetDirectoryName(main.Main.FilePath));
            }
            else
            {
                Process.Start("explorer.exe");
            }
        });
    }
}
