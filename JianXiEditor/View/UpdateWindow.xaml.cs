using JianXiEditor.Utility;

namespace JianXiEditor.View
{
    /// <summary>
    /// UpdateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateWindow
    {
        public UpdateWindow()
        {
            InitializeComponent();
            SystemMenuHandle removeSystemMenu = new SystemMenuHandle(this);
            SourceInitialized += removeSystemMenu.OnSourceInitialized;
        }
    }
}
