using JianXiEditor.Config;
using JianXiEditor.Utility;
using JianXiEditor.ViewModel;
using MiniConfigure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace JianXiEditor.Service
{
    /// <summary>
    /// 历史打开的文件
    /// </summary>
    class HistoryFile
    {
        /// <summary>
        /// 保存文件到历史文件夹
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void SaveHistoryList(string filePath)
        {
            try
            {
                if (!Directory.Exists(Config_Temp.AppDataPath_History))//如果不存在这个文件夹
                {
                    Directory.CreateDirectory(Config_Temp.AppDataPath_History);//那么就先创建一个
                }
                string sinPath = Config_Temp.AppDataPath_History + "FilePath.sin";//存放历史文件路径的文件路径（QAQ）
                if (File.Exists(sinPath))//如果存在该文件，就追加
                {
                    string fileContent = File.ReadAllText(sinPath);
                    if (!fileContent.Contains("[History]Time:"))//如果不存在日期，就添加一个当前的日期
                    {
                        MiniConfig.AppendConfig(sinPath, "History", "Time", DateTime.Now.ToString("yyyyMMdd"));
                    }
                    else//如果存在日期，那么需要做一些处理
                    {
                        int oldTime = int.Parse(MiniValue.GetValue(sinPath, "History", "Time"));
                        int newTime = int.Parse(DateTime.Now.ToString("yyyyMMdd"));

                        if (newTime > oldTime && ((newTime - oldTime) >= 3))//如果大于等于3，说明时间过去了三天，需要清理掉旧的path，以方便添加新path
                        {
                            File.WriteAllText(sinPath, "", Encoding.UTF8);//用空内容替换原先的内容
                            MiniConfig.AppendConfig(sinPath, "History", "Time", DateTime.Now.ToString("yyyyMMdd"));//清理掉后，重新将当前的系统日期添加到sin文件中
                        }
                    }
                    if (!fileContent.Contains(filePath))//如果该文件路径在sin文件中不存在，那么就添加进去
                    {
                        List<string> pathList = MiniValue.GetValueList(sinPath, "History", "Path");
                        foreach (var item in pathList)
                        {
                            if (!File.Exists(item))//如果该文件路径已失效
                            {
                                fileContent = fileContent.Replace("[History]Path:" + item + ";", "");//就删掉该条路径
                                File.WriteAllText(sinPath, fileContent.Replace("\r\n\r\n", "\r\n"), Encoding.UTF8);//再添加路径进去（同时清理一次空行）
                            }
                        }
                        if (pathList.Count >= 10)//如果当前文件中那个已经存在超过十个路径
                        {
                            fileContent = fileContent.Replace("[History]Path:" + pathList[0] + ";", "");//就删掉一个
                            File.WriteAllText(sinPath, fileContent.Replace("\r\n\r\n", "\r\n"), Encoding.UTF8);//再添加路径进去（同时清理一次空行）
                        }
                        MiniConfig.AppendConfig(sinPath, "History", "Path", filePath);
                    }
                }
                else//不存在文件，需要新建一个
                {
                    MiniConfig.AppendConfig(sinPath, "History", "Path", filePath);
                    MiniConfig.AppendConfig(sinPath, "History", "Time", DateTime.Now.ToString("yyyyMMdd"));
                }

            }
            catch (Exception ex)
            {
                //记录错误日志
                HandyControl.Tools.Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("写入历史文件时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        }
        /// <summary>
        /// 加载历史文件并创建控件列表
        /// </summary>
        /// <param name="contextMenu">右键菜单</param>
        public void LoadHistoryList(ContextMenu contextMenu)
        {
            if (Directory.Exists(Config_Temp.AppDataPath_History) && File.Exists(Config_Temp.AppDataPath_History + "FilePath.sin"))//如果存在这个文件夹和文件，再继续执行代码
            {
                List<string> direList = MiniValue.GetValueList(Config_Temp.AppDataPath_History + "FilePath.sin", "History", "Path");
                Thickness padding = new Thickness(5, 10, 5, 10);
                contextMenu.Items.Clear();//先清空一次，否则可能会出现添加多次的情况
                int i = 0;
                foreach (string item in direList)
                {
                    if (File.Exists(item))
                    {
                        MenuItem menuItem = new MenuItem
                        {
                            Name = "item" + i,
                            Header = item,
                            Padding = padding,
                        };
                        menuItem.Click += MenuItem_Click;
                        contextMenu.Items.Add(menuItem);
                        i++;
                    }
                }
                if (contextMenu.Items.Count > 0)//如果有子项才会显示菜单
                {
                    contextMenu.Visibility = Visibility.Visible;
                }
                else
                {
                    contextMenu.Visibility = Visibility.Hidden;
                }
            }
        }
        /// <summary>
        /// 右键菜单按钮的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string path = (sender as MenuItem).Header.ToString();
            MainViewModel main = (MainViewModel)Application.Current.MainWindow.DataContext;
            using (FileManager fileOpeare = new FileManager())
            {
                fileOpeare.DropDrapOpenFile(main.Main, main.Editor, path);
            }
        }
    }
}
