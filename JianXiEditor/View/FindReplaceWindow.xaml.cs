using JianXiEditor.Utility;
using JianXiEditor.ViewModel;

namespace JianXiEditor.View
{
    /// <summary>
    /// FindReplaceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FindReplaceWindow
    {
        public FindReplaceWindow(string selectContent)
        {
            InitializeComponent();
            FindReplaceViewModel findReplaceViewModel = (FindReplaceViewModel)DataContext;
            findReplaceViewModel.FindReplace.FindContent = selectContent;
            //去掉系统菜单
            SystemMenuHandle removeSystemMenu = new SystemMenuHandle(this);
            SourceInitialized += removeSystemMenu.OnSourceInitialized;
        }
    }
}
