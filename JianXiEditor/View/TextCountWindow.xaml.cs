using JianXiEditor.Utility;
using JianXiEditor.ViewModel;

namespace JianXiEditor.View
{
    /// <summary>
    /// TextCountWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TextCountWindow
    {
        public TextCountWindow(bool isSelect)
        {
            InitializeComponent();
            SystemMenuHandle removeSystemMenu = new SystemMenuHandle(this);
            SourceInitialized += removeSystemMenu.OnSourceInitialized;
            //将主窗口选择的语法高亮的名字，作为参数传过来，然后再给这个view对应的viewmodel
            TextCountViewModel textCountVM = (TextCountViewModel)DataContext;
            textCountVM.TextCountM.IsSelect = isSelect;
        }
    }
}
