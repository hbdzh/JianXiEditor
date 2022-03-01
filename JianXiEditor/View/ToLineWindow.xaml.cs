using JianXiEditor.Utility;

namespace JianXiEditor.View
{
    /// <summary>
    /// ToLineWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ToLineWindow
    {
        public ToLineWindow()
        {
            InitializeComponent();
            //去掉系统菜单
            SystemMenuHandle removeSystemMenu = new SystemMenuHandle(this);
            SourceInitialized += removeSystemMenu.OnSourceInitialized;
        }
    }
}
