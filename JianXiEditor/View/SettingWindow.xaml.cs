using JianXiEditor.Utility;

namespace JianXiEditor.View
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow 
    {
        public SettingWindow()
        {
            InitializeComponent();
            SystemMenuHandle removeSystemMenu = new SystemMenuHandle(this);
            SourceInitialized += removeSystemMenu.OnSourceInitialized;
        }
    }
}
