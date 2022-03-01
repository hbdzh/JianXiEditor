using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using System.Collections.Generic;
using HandyControl.Tools;

namespace JianXiEditor.Model
{
    class EditorModel : BindablePropertyBase
    {
        private readonly TextEditorOptions textOptions;
        public EditorModel()
        {
            textOptions = new TextEditorOptions();
        }
        public TextEditorOptions TextOptions
        {
            get => textOptions;
        }

        #region TextEditor
        /// <summary>
        /// 字体大小列表
        /// </summary>
        public List<double> FontSizeList { get; } = new List<double>()
        {
            8,
            9,
            10,
            11,
            12,
            14,
            16,
            18,
            20,
            24,
            28,
            36,
            48
        };
        private double selectFontSize;
        /// <summary>
        /// 选择的字体大小
        /// </summary>
        public double SelelctFontSize
        {
            get => selectFontSize;
            set => Set(ref selectFontSize, value);
        }
        private bool wordWrap;
        /// <summary>
        /// 自动换行
        /// </summary>
        public bool WordWrap
        {
            get => wordWrap;
            set => Set(ref wordWrap, value);
        }
        private bool showLineNumbers;
        /// <summary>
        /// 行号显示
        /// </summary>
        public bool ShowLineNumbers
        {
            get => showLineNumbers;
            set => Set(ref showLineNumbers, value);
        }
        private IHighlightingDefinition syntaxHighlighting;
        /// <summary>
        /// 语法高亮格式
        /// </summary>
        public IHighlightingDefinition SyntaxHighlighting
        {
            get => syntaxHighlighting;
            set => Set(ref syntaxHighlighting, value);
        }
        #endregion
    }
}
