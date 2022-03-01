using HandyControl.Tools;
using HandyControl.Tools.Command;
using JianXiEditor.Model;
using System.Windows;

namespace JianXiEditor.ViewModel
{
    class EncodingViewModel : BindablePropertyBase
    {
        private EncodingModel encodingM;
        public EncodingModel EncodingM
        {
            get
            {
                if (encodingM == null)//如果不是空的，就说明已经实例化了
                {
                    encodingM = new EncodingModel();
                }
                return encodingM;
            }
            set => Set(ref encodingM, value);
        }
        /// <summary>
        /// 切换编码格式
        /// </summary>
        public RelayCommand SelectEncodingCommand => new RelayCommand(obj =>
        {
            MainViewModel main = Application.Current.MainWindow.DataContext as MainViewModel;
            if (main.Main.FileEncoding == EncodingM.SelectEncoding)//如果编码格式一致就啥也不干了
            {
                return;
            }
            else
            {
                main.Main.FileEncoding = EncodingM.SelectEncoding;
                main.Main.SaveStatus = "未保存";
            }
        });
    }
}
