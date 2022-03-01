using HandyControl.Tools;
using ICSharpCode.AvalonEdit.Document;
namespace JianXiEditor.Model
{
    class MainModel : BindablePropertyBase
    {
        private TextDocument document;
        /// <summary>
        /// 文件内容
        /// </summary>
        public TextDocument Document
        {
            get => document;
            set => Set(ref document, value);
        }
        private string filePath;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            get => filePath;
            set => Set(ref filePath, value);
        }
        private string fileSyntaxHighlighting;
        /// <summary>
        /// 高亮语言
        /// </summary>
        public string FileSyntaxHighlighting
        {
            get => fileSyntaxHighlighting;
            set => Set(ref fileSyntaxHighlighting, value);
        }
        private string wordCountInfo;
        /// <summary>
        /// 详细字数统计
        /// </summary>
        public string WordCountInfo
        {
            get => wordCountInfo;
            set => Set(ref wordCountInfo, value);
        }
        private string selectWordCountInfo;
        /// <summary>
        /// 详细的已选中的字数统计
        /// </summary>
        public string SelectWordCountInfo
        {
            get => selectWordCountInfo;
            set => Set(ref selectWordCountInfo, value);
        }
        private string fileEncoding;
        /// <summary>
        /// 编码格式
        /// </summary>
        public string FileEncoding
        {
            get => fileEncoding;
            set => Set(ref fileEncoding, value);
        }
        private string fileLineBreak;
        /// <summary>
        /// 换行符格式
        /// </summary>
        public string FileLineBreak
        {
            get => fileLineBreak;
            set => Set(ref fileLineBreak, value);
        }
        private string saveStatus;
        /// <summary>
        /// 保存状态
        /// </summary>
        public string SaveStatus
        {
            get => saveStatus;
            set => Set(ref saveStatus, value);
        }
        private int selectTextLength;
        /// <summary>
        /// 选择的文字数
        /// </summary>
        public int SelectTextLength
        {
            get => selectTextLength;
            set => Set(ref selectTextLength, value);
        }
        private string lineHeight;
        /// <summary>
        /// 行高
        /// </summary>
        public string LineHeight
        {
            get => lineHeight;
            set => Set(ref lineHeight, value);
        }
    }
}
