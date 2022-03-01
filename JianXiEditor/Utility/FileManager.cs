using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using JianXiEditor.Config;
using JianXiEditor.Model;
using JianXiEditor.Service;
using JianXiEditor.View;
using Microsoft.Win32;
using MiniConfigure;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using MessageBox = HandyControl.Controls.MessageBox;

namespace JianXiEditor.Utility
{
    class FileManager : IDisposable
    {
        ContextMenu contextMenu;
        public FileManager()
        {
            contextMenu = ((MainWindow)Application.Current.MainWindow).HistoryListMenu;
        }
        /// <summary>
        /// 新建文件（代码复用）
        /// </summary>
        /// <param name="main"></param>
        /// <param name="editor"></param>
        private void NewFile2(MainModel main, EditorModel editor)
        {
            string[] backUpFiles = Directory.GetFiles(Config_Temp.AppDataPath_BackUp);
            if (backUpFiles.Length > 0)//如果有备份文件，就删掉
            {
                foreach (var item in backUpFiles)
                {
                    File.Delete(item);
                }
            }
            main.Document = new TextDocument();
            Config_Temp.FileContent = null;
            main.FilePath = "新文本文件.txt";//设置路径
            main.SaveStatus = "未保存";//设置文件变更状态
            main.FileLineBreak = "CRLF";//设置换行符格式
            main.FileEncoding = "UTF-8";//设置编码格式
            main.FileSyntaxHighlighting = "Text";
            editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(".txt");//设置高亮规则
        }
        /// <summary>
        /// 打开文件（代码复用）
        /// </summary>
        /// <param name="main"></param>
        /// <param name="editor"></param>
        /// <param name="filePath"></param>
        private void OpenFile2(MainModel main, EditorModel editor, string filePath)
        {
            //将文件读取以后，存储到一个属性中
            Config_Temp.FileContent = null;
            Config_Temp.FileContent = new FileReadWrite().ReadFileContent(filePath, FileBasicInfo.GetEncodingName(filePath)).ToString();
            //设置Document，也就是将内容展示到文本框上
            main.Document = new TextDocument(Config_Temp.FileContent);
            //状态栏设置
            main.FilePath = filePath;//设置路径
            main.SaveStatus = "已保存";//设置文件变更状态
            main.FileEncoding = FileBasicInfo.GetEncodingName(filePath);//设置编码格式
            main.FileLineBreak = FileBasicInfo.GetLineBreak(Config_Temp.FileContent);//设置换行符格式
            main.FileSyntaxHighlighting = FileExtension.ToName(Path.GetExtension(filePath));//显示在状态栏的语法名
            editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(FileExtension.ToExtension(main.FileSyntaxHighlighting));//设置高亮规则
            HistoryFile historyFile = new HistoryFile();
            historyFile.SaveHistoryList(filePath);//保存到历史文件
            historyFile.LoadHistoryList(contextMenu);//加载文件到右键菜单
        }
        /// <summary>
        /// 保存文件（代码复用）
        /// </summary>
        /// <param name="main"></param>
        /// <param name="filePath"></param>
        public void SaveFile2(MainModel main, string filePath)
        {
            //状态栏设置
            string fileContent = main.Document.Text;
            string currentLineBreak = FileBasicInfo.GetLineBreakName(FileBasicInfo.GetLineBreak(fileContent));//当前换行符格式
            string newLineBreak = FileBasicInfo.GetLineBreakName(main.FileLineBreak);//新换行符格式
            if (currentLineBreak == newLineBreak)
            {
                fileContent = fileContent.Replace(currentLineBreak, newLineBreak);//修改换行符格式之后，再保存
            }
            new FileReadWrite().WriteFileContent(filePath, fileContent, FileBasicInfo.GetEncoding(main.FileEncoding));
            Config_Temp.FileContent = null;
            Config_Temp.FileContent = fileContent;
            main.SaveStatus = "已保存";//设置文件变更状态
            HistoryFile historyFile = new HistoryFile();
            historyFile.SaveHistoryList(filePath);//保存到历史文件
            historyFile.LoadHistoryList(contextMenu);//加载文件到右键菜单
        }
        /// <summary>
        /// 新建文件
        /// </summary>
        /// <param name="main"></param>
        /// <param name="editor"></param>
        public void NewFile(MainModel main, EditorModel editor)
        {
            if (!File.Exists(main.FilePath) && string.IsNullOrEmpty(main.Document.Text))
            {
                NewFile2(main, editor);
            }
            else
            {
                if (main.SaveStatus == "已保存")
                {
                    NewFile2(main, editor);
                }
                else
                {
                    switch (MessageBox.Show("如果不保存，您所做的修改将会丢失", "是否保存更改？", MessageBoxButton.YesNoCancel, MessageBoxImage.Information))
                    {
                        case MessageBoxResult.Yes:
                            if (SaveFile(main, editor) == true)
                            {
                                NewFile2(main, editor);
                            }
                            break;
                        case MessageBoxResult.No:
                            NewFile2(main, editor);
                            break;
                        case MessageBoxResult.Cancel:
                            return;
                    }
                }
            }
        }
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="main"></param>
        /// <param name="editor"></param>
        public void OpenFile(MainModel main, EditorModel editor)
        {
            OpenFileDialog ofd = FileExplorer.OpenFileDialogWindow();

            if ((main.SaveStatus == "已保存") || (!File.Exists(main.FilePath) && string.IsNullOrEmpty(main.Document.Text)))
            {
                if (ofd.ShowDialog() == true)//如果返回true，说明选择了文件
                {
                    //注：这里建议再做一次判断，判断一下，选择的文件是否存在
                    if (File.Exists(ofd.FileName))
                    {
                        OpenFile2(main, editor, ofd.FileName);
                    }
                }
            }
            else
            {
                switch (MessageBox.Show("如果不保存，您所做的修改将会丢失", "是否保存更改？", MessageBoxButton.YesNoCancel, MessageBoxImage.Information))
                {
                    case MessageBoxResult.Yes:
                        if (SaveFile(main, editor) == true)
                        {
                            OpenFile(main, editor);
                        }
                        break;
                    case MessageBoxResult.No:
                        main.SaveStatus = "已保存";
                        OpenFile(main, editor);
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }
        }
        /// <summary>
        /// 拖拽打开文件
        /// </summary>
        /// <param name="main"></param>
        /// <param name="editor"></param>
        /// <param name="filePath"></param>
        public void DropDrapOpenFile(MainModel main, EditorModel editor, string filePath)
        {
            if ((main.SaveStatus == "已保存") || (!File.Exists(main.FilePath) && string.IsNullOrEmpty(main.Document.Text)))
            {
                //注：这里建议再做一次判断，判断一下，选择的文件是否存在
                if (File.Exists(filePath))
                {
                    OpenFile2(main, editor, filePath);
                }
            }
            else
            {
                switch (MessageBox.Show("如果不保存，您所做的修改将会丢失", "是否保存更改？", MessageBoxButton.YesNoCancel, MessageBoxImage.Information))
                {
                    case MessageBoxResult.Yes:
                        if (SaveFile(main, editor) == true)
                        {
                            DropDrapOpenFile(main, editor, filePath);
                        }
                        break;
                    case MessageBoxResult.No:
                        main.SaveStatus = "已保存";
                        OpenFile(main, editor);
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }
        }
        /// <summary>
        /// 文件保存
        /// </summary>
        /// <param name="main"></param>
        /// <param name="editor"></param>
        /// <returns>true保存成功，false没有保存</returns>
        public bool SaveFile(MainModel main, EditorModel editor)
        {
            if (File.Exists(main.FilePath))//如果存在文件，就允许保存
            {
                SaveFile2(main, main.FilePath);
                return true;
            }
            else//否则调用另存为
            {
                return SaveAsFile(main, editor);
            }
        }
        /// <summary>
        /// 文件另存为
        /// </summary>
        /// <param name="main"></param>
        /// <param name="editor"></param>
        /// <returns>true保存成功，false没有保存</returns>
        public bool SaveAsFile(MainModel main, EditorModel editor)
        {
            SaveFileDialog sfd = FileExplorer.SaveFileDialogWindow(Path.GetFileName(main.FilePath));
            if (sfd.ShowDialog() == true)//如果返回true，说明选择了文件
            {
                SaveFile2(main, sfd.FileName);
                main.FilePath = sfd.FileName;//保存后重新设置路径
                main.FileEncoding = FileBasicInfo.GetEncodingName(sfd.FileName);//设置编码格式
                main.FileLineBreak = FileBasicInfo.GetLineBreak(Config_Temp.FileContent);//设置换行符格式
                main.FileSyntaxHighlighting = FileExtension.ToName(Path.GetExtension(sfd.SafeFileName));//显示在状态栏的语法名
                editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(FileExtension.ToExtension(main.FileSyntaxHighlighting));//设置高亮规则
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 退出前的保存操作
        /// </summary>
        /// <param name="main"></param>
        /// <param name="editor"></param>
        /// <returns>true是可以关闭，false不可以关闭</returns>
        public bool ExitFileSave(MainModel main, EditorModel editor)
        {
            string fileContent = main.Document.Text;
            if ((fileContent == Config_Temp.FileContent) || fileContent == "" && Config_Temp.FileContent == null && main.SaveStatus == "已保存")
            {
                return true;
            }
            else if (main.SaveStatus == "未保存" && Config_Temp.FileContent == null && fileContent == "")
            {
                return true;
            }
            else
            {
                switch (MessageBox.Show("如果不保存，您所做的修改将会丢失", "是否保存更改？", MessageBoxButton.YesNoCancel, MessageBoxImage.Information))
                {
                    case MessageBoxResult.Yes:
                        //注：这里建议再做一次判断，判断一下，选择的文件是否存在
                        if (File.Exists(main.FilePath))
                        {
                            string currentLineBreak = FileBasicInfo.GetLineBreakName(FileBasicInfo.GetLineBreak(fileContent));//当前换行符格式
                            string newLineBreak = FileBasicInfo.GetLineBreakName(main.FileLineBreak);//新换行符格式
                            if (currentLineBreak == newLineBreak)
                            {
                                fileContent = fileContent.Replace(currentLineBreak, newLineBreak);//修改换行符格式之后，再保存
                            }
                            new FileReadWrite().WriteFileContent(main.FilePath, fileContent, FileBasicInfo.GetEncoding(main.FileEncoding));
                            new HistoryFile().SaveHistoryList(main.FilePath);//保存到历史文件
                            return true;
                        }
                        else
                        {
                            if (SaveAsFile(main, editor) == true)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    case MessageBoxResult.No:
                        return true;
                    case MessageBoxResult.Cancel:
                        return false;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// 退出前备份文件
        /// </summary>
        /// <param name="main"></param>
        public void ExitFileBackUp(MainModel main)
        {
            string fileContent = main.Document.Text;
            if (fileContent.Length > 0)
            {
                if (File.Exists(main.FilePath))
                {
                    MiniConfig.CreateFile(Config_Temp.AppDataPath_BackUp + "Backup.sin", "File", "Path", main.FilePath);
                    string currentLineBreak = FileBasicInfo.GetLineBreakName(FileBasicInfo.GetLineBreak(fileContent));//当前换行符格式
                    string newLineBreak = FileBasicInfo.GetLineBreakName(main.FileLineBreak);//新换行符格式
                    if (currentLineBreak == newLineBreak)
                    {
                        fileContent = fileContent.Replace(currentLineBreak, newLineBreak);//修改换行符格式之后，再保存
                    }
                    new FileReadWrite().WriteFileContent(main.FilePath, fileContent, FileBasicInfo.GetEncoding(main.FileEncoding));
                }
                else
                {
                    string[] backUpFiles = Directory.GetFiles(Config_Temp.AppDataPath_BackUp, "*.backup");
                    if (backUpFiles.Length > 0)//如果已经有其他备份文件，就删掉
                    {
                        foreach (var item in backUpFiles)
                        {
                            File.Delete(item);
                        }
                    }
                    new FileReadWrite().WriteFileContent(Config_Temp.AppDataPath_BackUp + DateTime.Now.ToString("yyyyMMddHHmmss") + ".backup", fileContent, FileBasicInfo.GetEncoding(main.FileEncoding));
                }
            }
        }
        /// <summary>
        /// 这样调用GC可以减少卡顿
        /// </summary>
        public void Dispose()
        {
            Task.Run(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                    new Action(() =>
                    {
                        GC.Collect();
                    }));
            });
        }
    }
}
