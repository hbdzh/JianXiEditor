using HandyControl.Tools;
using HandyControl.Tools.Command;
using JianXiEditor.Config;
using JianXiEditor.Service;
using JianXiEditor.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JianXiEditor.ViewModel
{
    class UpdateViewModel : BindablePropertyBase
    {
        private UpdateModel updateVM;
        public UpdateModel UpdateVM
        {
            get
            {
                if (updateVM == null)//如果不是空的，就说明已经实例化了
                {
                    updateVM = new UpdateModel();
                }
                return updateVM;
            }
            set => Set(ref updateVM, value);
        }
        /// <summary>
        /// 窗口初始化时
        /// </summary>
        public RelayCommand UpdateWindowLoadCommand => new RelayCommand(obj =>
        {
            //初始化
            Task.Run(() =>
            {
                UpdateVM.CurrentVersion = UpdateManager.LocalVersion;
                UpdateVM.LatestVersion = UpdateManager.LatestVersion;
                UpdateVM.ProgressMaximum = 100;
                UpdateVM.UpdateProgress = 0;
                StringBuilder sb = new StringBuilder();
                foreach (string item in UpdateManager.LatestUpdateContent)
                {
                    sb.AppendLine(item);
                }
                UpdateVM.UpdateContent = sb.ToString();
            });
        });
        private Window window;
        /// <summary>
        /// 开始更新
        /// </summary>
        public RelayCommand UpdateConfirmCommand => new RelayCommand(new Action<object>(obj =>
        {
            window = obj as Window;
            if (UpdateVM.UpdateProgress != 0)//防止用户在点过一次是，新版本开始下载后，又点了一次是
            {
                return;
            }
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
                wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
                Uri downloadLink = new Uri("http://download.meixiapp.com/JianXiEditor/Files/" + Config_Temp.AppSetupName);
                wc.DownloadFileAsync(downloadLink, Config_Temp.AppDataPath_Update + Config_Temp.AppSetupName);
            }
        }));
        /// <summary>
        /// 下载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            UpdateVM.ProgressMaximum = (int)e.TotalBytesToReceive;
            UpdateVM.UpdateProgress = (int)e.BytesReceived;
        }
        /// <summary>
        /// 下载完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            /*当前用户是管理员的时候，直接启动应用程序
            如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行*/

            //判断当前用户是否为管理员
            if (ApplicationHelper.IsAdministrator())
            {
                //如果是管理员，则直接运行
                Process.Start(Config_Temp.AppDataPath_Update + Config_Temp.AppSetupName);
                if (window != null)
                {
                    window.Close();
                }
            }
            else
            {
                //创建启动对象
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    WorkingDirectory = Config_Temp.AppDataPath_Update,
                    FileName = Config_Temp.AppDataPath_Update + Config_Temp.AppSetupName,
                    //设置启动动作,确保以管理员身份运行
                    Verb = "runas"
                };
                Process.Start(startInfo);

                if (window != null)
                {
                    window.Close();
                }
            }
            if (File.Exists(Config_Temp.AppDataPath_Update + "Update.sin"))
            {
                File.Delete(Config_Temp.AppDataPath_Update + "Update.sin");
            }
        }
        /// <summary>
        /// 取消更新
        /// </summary>
        public RelayCommand UpdateCancelCommand => new RelayCommand(new Action<object>(obj =>
        {
            if (UpdateVM.UpdateProgress != 0)//防止用户在点过一次是否，新版本开始下载后，又点了一次否
            {
                return;
            }
            if (File.Exists(Config_Temp.AppDataPath_Update + "Update.sin"))
            {
                File.Delete(Config_Temp.AppDataPath_Update + "Update.sin");
            }
            window = obj as Window;
            if (window != null)
            {
                window.Close();
            }
        }));
    }
}
