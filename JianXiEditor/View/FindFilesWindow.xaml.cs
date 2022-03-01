using JianXiEditor.Utility;

namespace JianXiEditor.View
{
    /// <summary>
    /// FindFilesWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FindFilesWindow 
    {
        public FindFilesWindow()
        {
            InitializeComponent();
            SystemMenuHandle removeSystemMenu = new SystemMenuHandle(this);
            SourceInitialized += removeSystemMenu.OnSourceInitialized;
        }
    }
}
