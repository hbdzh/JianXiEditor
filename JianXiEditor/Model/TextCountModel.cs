using HandyControl.Tools;
namespace JianXiEditor.Model
{
    class TextCountModel : BindablePropertyBase
    {
        private bool isSelect;
        /// <summary>
        /// 是不是选中文字
        /// </summary>
        public bool IsSelect
        {
            get => isSelect;
            set => Set(ref isSelect, value);
        }
        private int totalWord;
        /// <summary>
        /// 总字数（不含空格和换行等）
        /// </summary>
        public int TotalWord
        {
            get => totalWord;
            set => Set(ref totalWord, value);
        }
        private int byteCount;
        /// <summary>
        /// 字节数（不含空格和换行等）
        /// </summary>
        public int ByteCount
        {
            get => byteCount;
            set => Set(ref byteCount, value);
        }
        private int chineseCount;
        /// <summary>
        /// 汉字数量
        /// </summary>
        public int ChineseCount
        {
            get => chineseCount;
            set => Set(ref chineseCount, value);
        }
        private int numberCount;
        /// <summary>
        /// 数字数量
        /// </summary>
        public int NumberCount
        {
            get => numberCount;
            set => Set(ref numberCount, value);
        }
        private int letterCount;
        /// <summary>
        /// 字母数量
        /// </summary>
        public int LetterCount
        {
            get => letterCount;
            set => Set(ref letterCount, value);
        }
        private int symbolCount;
        /// <summary>
        /// 符号数量
        /// </summary>
        public int SymbolCount
        {
            get => symbolCount;
            set => Set(ref symbolCount, value);
        }
    }
}
