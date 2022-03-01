using HandyControl.Tools;

namespace JianXiEditor.Model
{
    class FilePathModel : BindablePropertyBase
    {
        private string fileName;
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get => fileName;
            set => Set(ref fileName, value);
        }
    }
}
