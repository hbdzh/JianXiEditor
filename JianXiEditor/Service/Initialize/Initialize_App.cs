using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using JianXiEditor.Config;
using JianXiEditor.Model;
using JianXiEditor.Utility;
using MiniConfigure;
using System;
using System.IO;

namespace JianXiEditor.Service.Initialize
{
    class Initialize_App
    {
        /// <summary>
        /// 代码复用
        /// </summary>
        /// <param name="main"></param>
        /// <param name="editor"></param>
        /// <param name="filePath"></param>
        private void OpenFile(MainModel main, EditorModel editor, string filePath)
        {
            //将文件读取以后，存储到一个属性中
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
        }
        /// <summary>
        /// 默认情况下的初始化操作
        /// </summary>
        private void EditorDefaultInit(MainModel main, EditorModel editor)
        {
            main.FilePath = "新文本文件.txt";//设置路径
            main.SaveStatus = "未保存";//设置文件变更状态
            main.FileLineBreak = "CRLF";//设置换行符格式
            main.FileEncoding = "UTF-8";//设置编码格式
            main.FileSyntaxHighlighting = "Text";
            editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(".txt");//设置高亮规则
            main.Document = new TextDocument();
            Config_Temp.FileContent = null;
        }
        /// <summary>
        /// 判断是打开备份文件还是新建一个
        /// </summary>
        /// <param name="main"></param>
        /// <param name="editor"></param>
        private void OpenBackUpFile(MainModel main, EditorModel editor)
        {
            if (Config_App.Config_FileCache == true)
            {
                if (File.Exists(Config_Temp.AppDataPath_BackUp + "Backup.sin"))
                {
                    string filePath = MiniValue.GetValue(Config_Temp.AppDataPath_BackUp + "Backup.sin", "File", "Path");
                    if (File.Exists(filePath))
                    {
                        OpenFile(main, editor, filePath);
                    }
                    else//没有备份文件时就打开关联文件
                    {
                        EditorDefaultInit(main, editor);//调用默认的文件基本信息设置
                        File.Delete(Config_Temp.AppDataPath_BackUp + "Backup.sin");
                    }
                }
                else
                {
                    string[] backUpFiles = Directory.GetFiles(Config_Temp.AppDataPath_BackUp, "*.backup");
                    if (backUpFiles.Length > 0)
                    {
                        foreach (var item in backUpFiles)
                        {
                            OpenFile(main, editor, item);
                            main.FilePath = Path.GetFileName(item);
                            main.SaveStatus = "未保存";
                            Config_Temp.FileContent = null;
                        }
                    }
                    else
                    {
                        EditorDefaultInit(main, editor);//调用默认的文件基本信息设置
                    }
                }
            }
            else
            {
                EditorDefaultInit(main, editor);//调用默认的文件基本信息设置
            }
        }
        /// <summary>
        /// 程序初始化时判断是打开备份文件还是关联文件
        /// </summary>
        /// <param name="main"></param>
        /// <param name="editor"></param>
        public void InitBackUpOrAssociated(MainModel main, EditorModel editor)
        {
            try
            {
                string filePath = null;
                string[] openFilePath = Environment.GetCommandLineArgs();
                if (openFilePath.Length > 2)//如果文件名中存在空格，则需要处理一下
                {
                    for (int i = 1; i < openFilePath.Length; i++)
                    {
                        filePath += openFilePath[i] + " ";
                    }
                }
                else if (openFilePath.Length == 2)//文件名中没有空格，直接使用就行
                {
                    filePath = openFilePath[1];
                }
                else//没有打开文件，就调用默认的文件基本信息设置
                {
                    OpenBackUpFile(main, editor);
                    return;
                }
                if (File.Exists(filePath))
                {
                    OpenFile(main, editor, filePath);
                    //打开关联文件后，需要删除备份文件
                    string[] files = Directory.GetFiles(Config_Temp.AppDataPath_BackUp);
                    foreach (string item in files)
                    {
                        File.Delete(item);
                    }
                }
                else
                {
                    OpenBackUpFile(main, editor);//调用默认的文件基本信息设置
                }
            }
            catch (Exception ex)
            {
                //记录错误日志
                HandyControl.Tools.Logger.Log(ex);
                OpenBackUpFile(main, editor);//调用默认的文件基本信息设置
                HandyControl.Controls.Growl.Warning("打开关联/备份文件时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        }
    }
}