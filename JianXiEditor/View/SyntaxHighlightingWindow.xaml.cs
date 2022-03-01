using JianXiEditor.Utility;
using JianXiEditor.ViewModel;

namespace JianXiEditor.View
{
    /// <summary>
    /// SyntaxHighlightingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SyntaxHighlightingWindow
    {
        public SyntaxHighlightingWindow(string languageName)
        {
            InitializeComponent();
            //将主窗口选择的语法高亮的名字，作为参数传过来，然后再给这个view对应的viewmodel
            SyntaxHighlightingViewModel syntax = (SyntaxHighlightingViewModel)DataContext;
            syntax.Syntax.SelectProgramLanguage = languageName;
            SystemMenuHandle removeSystemMenu = new SystemMenuHandle(this);
            SourceInitialized += removeSystemMenu.OnSourceInitialized;
        }
    }
}
