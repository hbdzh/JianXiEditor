using HandyControl.Tools;

namespace JianXiEditor.Model
{
    class FindReplaceModel : BindablePropertyBase
    {
        private string findContent;
        /// <summary>
        /// 要查找的内容
        /// </summary>
        public string FindContent
        {
            get => findContent;
            set => Set(ref findContent, value);
        }
        private string replaceContent = "";
        /// <summary>
        /// 要替换的内容
        /// </summary>
        public string ReplaceContent
        {
            get => replaceContent;
            set => Set(ref replaceContent, value);
        }
        private string fileType;
        /// <summary>
        /// 指定的文件类型
        /// </summary>
        public string FileType
        {
            get => fileType;
            set => Set(ref fileType, value);
        }
        private bool regExp;
        /// <summary>
        /// 正则表达式
        /// </summary>
        public bool RegExp
        {
            get => regExp;
            set => Set(ref regExp, value);
        }
        private bool matchCase;
        /// <summary>
        /// 区分大小写
        /// </summary>
        public bool MatchCase
        {
            get => matchCase;
            set => Set(ref matchCase, value);
        }
        private bool matchWholeWord;
        /// <summary>
        /// 全字匹配
        /// </summary>
        public bool MatchWholeWord
        {
            get => matchWholeWord;
            set => Set(ref matchWholeWord, value);
        }
        private bool wildcards;
        /// <summary>
        /// 通配符
        /// </summary>
        public bool WildCards
        {
            get => wildcards;
            set => Set(ref wildcards, value);
        }
        private bool searchUp;
        /// <summary>
        /// 向上搜索
        /// </summary>
        public bool SearchUp
        {
            get => searchUp;
            set => Set(ref searchUp, value);
        }
        private bool inFolderFind;
        /// <summary>
        /// 在文件夹中查找
        /// </summary>
        public bool InFolderFind
        {
            get => inFolderFind;
            set => Set(ref inFolderFind, value);
        }
    }
}
