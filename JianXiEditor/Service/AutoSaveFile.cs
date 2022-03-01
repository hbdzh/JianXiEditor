using HandyControl.Controls;
using JianXiEditor.Config;
using JianXiEditor.Utility;
using JianXiEditor.ViewModel;
using System.IO;
using System.Timers;
namespace JianXiEditor.Service
{
    class AutoSaveFile
    {
        private Timer autoSave = new Timer(180000);
        private MainViewModel main;
        private string fileContent;
        /// <summary>
        /// 定时器初始化
        /// </summary>
        public void Initialize(MainViewModel mainViewModel)
        {
            main = mainViewModel;
            fileContent = main.Main.Document.Text;
            if (main.Setting.AutoSaveFile == true)
            {
                autoSave.Elapsed += AutoSave_Elapsed;
                autoSave.AutoReset = true;
                autoSave.Start();
            }
        }
        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSave_Elapsed(object sender, ElapsedEventArgs e)
        {
            autoSave.Stop();
            if (main.Setting.AutoSaveFile == true)
            {
                if (File.Exists(main.Main.FilePath))
                {
                    //状态栏设置
                    string currentLineBreak = FileBasicInfo.GetLineBreakName(FileBasicInfo.GetLineBreak(fileContent));//当前换行符格式
                    string newLineBreak = FileBasicInfo.GetLineBreakName(main.Main.FileLineBreak);//新换行符格式
                    if (currentLineBreak == newLineBreak)
                    {
                        fileContent = fileContent.Replace(currentLineBreak, newLineBreak);//修改换行符格式之后，再保存
                    }
                    new FileReadWrite().WriteFileContent(main.Main.FilePath, fileContent, FileBasicInfo.GetEncoding(main.Main.FileEncoding));
                    Config_Temp.FileContent = fileContent;
                    main.Main.SaveStatus = "已保存";//设置文件变更状态
                    Growl.Success("自动保存成功！");
                }
                else if (!File.Exists(main.Main.FilePath) && fileContent.Length > 0)
                {
                    Growl.Info("您的文件似乎还没有保存，建议保存！");
                }
                autoSave.Start();
            }
        }
    }
}