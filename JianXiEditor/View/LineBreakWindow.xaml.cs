using JianXiEditor.Utility;
using JianXiEditor.ViewModel;

namespace JianXiEditor.View
{
    /// <summary>
    /// LineBreakWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LineBreakWindow
    {
        public LineBreakWindow(string lineBreakName)
        {
            InitializeComponent();
            //将主窗口的换行符名字，作为参数传过来，然后再给这个view对应的viewmodel
            LineBreakViewModel lineBreak = (LineBreakViewModel)DataContext;
            lineBreak.Line.SelectLineBreak = lineBreakName;
            SystemMenuHandle removeSystemMenu = new SystemMenuHandle(this);
            SourceInitialized += removeSystemMenu.OnSourceInitialized;
        }
    }
}
