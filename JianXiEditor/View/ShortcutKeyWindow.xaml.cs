using JianXiEditor.Utility;

namespace JianXiEditor.View
{
    /// <summary>
    /// ShortcutKeyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShortcutKeyWindow
    {
        public ShortcutKeyWindow()
        {
            InitializeComponent();
            SystemMenuHandle removeSystemMenu = new SystemMenuHandle(this);
            SourceInitialized += removeSystemMenu.OnSourceInitialized;
        }
    }
}
