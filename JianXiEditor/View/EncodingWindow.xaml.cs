using JianXiEditor.Utility;
using JianXiEditor.ViewModel;

namespace JianXiEditor.View
{
    /// <summary>
    /// EncodingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EncodingWindow 
    {
        public EncodingWindow(string encodingName)
        {
            InitializeComponent();
            EncodingViewModel encoding = (EncodingViewModel)DataContext;
            encoding.EncodingM.SelectEncoding = encodingName;
            SystemMenuHandle removeSystemMenu = new SystemMenuHandle(this);
            SourceInitialized += removeSystemMenu.OnSourceInitialized;
        }
    }
}
