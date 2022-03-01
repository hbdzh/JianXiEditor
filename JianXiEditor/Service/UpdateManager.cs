using JianXiEditor.Config;
using MiniConfigure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace JianXiEditor.Service
{
    class UpdateManager
    {
        /// <summary>
        /// 获取本地版本
        /// </summary>
        /// <returns></returns>
        public static string LocalVersion => FileVersionInfo.GetVersionInfo(Config_Temp.AppPath + "简兮编辑器.exe").FileVersion;
        /// <summary>
        /// 获取更新文件
        /// </summary>
        /// <returns>true下载更新信息成功，false下载更新信息失败</returns>
        public bool GetUpdateFile()
        {
            if (!Directory.Exists(Config_Temp.AppDataPath_Update))
            {
                Directory.CreateDirectory(Config_Temp.AppDataPath_Update);
            }
            try
            {
                return HttpDownloadFile();
            }
            catch (Exception ex)
            {
                //记录错误日志
                HandyControl.Tools.Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("获取更新文件时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
                return false;
            }
        }
        /// <summary>
        /// 获取最新版本
        /// </summary>
        /// <returns></returns>
        public static string LatestVersion => MiniValue.GetValue(Config_Temp.AppDataPath_Update + "Update.sin", "Latest", "Version");
        /// <summary>
        /// 获取最新版本更新内容
        /// </summary>
        /// <returns></returns>
        public static List<string> LatestUpdateContent => MiniValue.GetValueList(Config_Temp.AppDataPath_Update + "Update.sin", "Latest", "Content");
        /// <summary>
        /// 从服务器下载文件
        /// </summary>
        /// <returns>下载是否成功</returns>
        private bool HttpDownloadFile()
        {
            try
            {
                HttpWebRequest request = WebRequest.Create("https://download.meixiapp.com/JianXiEditor/Update.sin") as HttpWebRequest;//设置参数
                request.Timeout = 5000;//超时等待5秒，默认是100秒
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)//发送请求并获取相应回应数据
                {
                    using (Stream responseStream = response.GetResponseStream())//直到request.GetResponse()程序才开始向目标网页发送Post请求
                    {
                        using (Stream stream = new FileStream(Config_Temp.AppDataPath_Update + "Update.sin", FileMode.Create))//创建本地文件写入流
                        {
                            byte[] bArr = new byte[1024];
                            int size = responseStream.Read(bArr, 0, bArr.Length);
                            while (size > 0)
                            {
                                stream.Write(bArr, 0, size);
                                size = responseStream.Read(bArr, 0, bArr.Length);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //记录错误日志
                HandyControl.Tools.Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("下载最新安装包时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
                return false;
            }
        }
    }
}
