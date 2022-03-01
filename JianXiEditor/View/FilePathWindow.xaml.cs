using JianXiEditor.Utility;
using JianXiEditor.ViewModel;
using System.IO;
namespace JianXiEditor.View
{
    /// <summary>
    /// FilePathWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FilePathWindow
    {
        public FilePathWindow(string filePath)
        {
            InitializeComponent();
            //将主窗口选择的文件路径，作为参数传过来，然后再给这个view对应的viewmodel
            FilePathViewModel filePathVM = (FilePathViewModel)DataContext;
            filePathVM.FilePath.FileName = Path.GetFileName(filePath);
            FilePathViewModel.TempFileName = filePathVM.FilePath.FileName;
            SystemMenuHandle removeSystemMenu = new SystemMenuHandle(this);
            SourceInitialized += removeSystemMenu.OnSourceInitialized;
        }
    }
}
