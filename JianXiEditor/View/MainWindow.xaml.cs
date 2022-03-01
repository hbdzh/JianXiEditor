using HandyControl.Tools;
using JianXiEditor.Config;
using JianXiEditor.Utility;

namespace JianXiEditor.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ApplicationHelper.StartProfileOptimization(Config_Temp.AppDataPath);//开启JIT即时编译
            //去掉系统菜单
            SystemMenuHandle removeSystemMenu = new SystemMenuHandle(this);
            SourceInitialized += removeSystemMenu.OnSourceInitialized; 
        }
    }
}
