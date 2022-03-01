using HandyControl.Tools;
using HandyControl.Tools.Command;
using JianXiEditor.Config;
using JianXiEditor.Model;
using System;
using System.Windows;
using JianXiEditor.Utility;

namespace JianXiEditor.ViewModel
{
    class LineBreakViewModel : BindablePropertyBase
    {
        private LineBreakModel line;
        public LineBreakModel Line
        {
            get
            {
                if (line == null)//如果不是空的，就说明已经实例化了
                {
                    line = new LineBreakModel();
                }
                return line;
            }
            set => Set(ref line, value);
        }
        public RelayCommand LineBreakSelectedCommand => new RelayCommand(obj =>
        {
            try
            {
                MainViewModel main = Application.Current.MainWindow.DataContext as MainViewModel;
                string fileContent = main.Main.Document.Text;
                string currentLineBreak = FileBasicInfo.GetLineBreakName(FileBasicInfo.GetLineBreak(fileContent));//当前换行符格式
                string newLineBreak = FileBasicInfo.GetLineBreakName(Line.SelectLineBreak);//新的换行符格式
                if (fileContent.Contains(currentLineBreak))//如果存在当前的换行符
                {
                    main.Main.Document.Text = fileContent.Replace(currentLineBreak, newLineBreak);//替换掉
                }
                main.Main.FileLineBreak = Line.SelectLineBreak;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("切换换行符时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        });
    }
}
