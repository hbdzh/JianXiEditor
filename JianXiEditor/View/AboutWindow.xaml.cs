using JianXiEditor.Utility;

namespace JianXiEditor.View
{
    /// <summary>
    /// AboutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AboutWindow
    {
        public AboutWindow()
        {
            InitializeComponent();
            SystemMenuHandle removeSystemMenu = new SystemMenuHandle(this);
            SourceInitialized += removeSystemMenu.OnSourceInitialized;
        }
    }
}
