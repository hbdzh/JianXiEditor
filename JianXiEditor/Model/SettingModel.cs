using HandyControl.Tools;
using System.Collections.Generic;

namespace JianXiEditor.Model
{
    class SettingModel : BindablePropertyBase
    {
        /// <summary>
        /// 字体列表
        /// </summary>
        public List<string> FontList { get; set; } = new List<string>();
        private string selectFont;
        /// <summary>
        /// 选择的字体
        /// </summary>
        public string SelectFont
        {
            get => selectFont;
            set => Set(ref selectFont, value);
        }
        /// <summary>
        /// 主题列表
        /// </summary>
        public List<string> AppThemeList { get; } = new List<string>()
        {
            "Light",
            "Gray",
            "Dark"
        };
        /// <summary>
        /// 编辑器位置列表
        /// </summary>
        public List<string> EditorLocationList { get; } = new List<string>()
        {
            "Left",
            "Center"
        };
        private string selectEditorLocation;
        /// <summary>
        /// 选择的编辑器位置
        /// </summary>
        public string SelectEditorLocation
        {
            get => selectEditorLocation;
            set => Set(ref selectEditorLocation, value);
        }
        private string selectAppTheme;
        /// <summary>
        /// 选择的主题
        /// </summary>
        public string SelectAppTheme
        {
            get => selectAppTheme;
            set => Set(ref selectAppTheme, value);
        }
        /// <summary>
        /// 行高列表
        /// </summary>
        public List<double> LineHeightList { get; } = new List<double>()
        {
            1.0,
            1.1,
            1.2,
            1.3,
            1.4,
            1.5
        };
        private double selectLineHeight;
        /// <summary>
        /// 选择的行高
        /// </summary>
        public double SelectLineHeight
        {
            get => selectLineHeight;
            set => Set(ref selectLineHeight, value);
        }
        private bool fastMoveText;
        /// <summary>
        /// 快速移动文字
        /// </summary>
        public bool FastMoveText
        {
            get => fastMoveText;
            set => Set(ref fastMoveText, value);
        }
        private bool fastMoveLine;
        /// <summary>
        /// 快速移动行
        /// </summary>
        public bool FastMoveLine
        {
            get => fastMoveLine;
            set => Set(ref fastMoveLine, value);
        }
        private bool quickCopyLine;
        /// <summary>
        /// 快速复制当前行
        /// </summary>
        public bool QuickCopyLine
        {
            get => quickCopyLine;
            set => Set(ref quickCopyLine, value);
        }
        private bool autoUpdate;
        /// <summary>
        /// 开启自动更新
        /// </summary>
        public bool AutoUpdate
        {
            get => autoUpdate;
            set => Set(ref autoUpdate, value);
        }
        private bool realDisplayState;
        /// <summary>
        /// 实时显示文件状态
        /// </summary>
        public bool RealDisplayState
        {
            get => realDisplayState;
            set => Set(ref realDisplayState, value);
        }
        private bool fileCache;
        /// <summary>
        /// 开启文件缓存
        /// </summary>
        public bool FileCache
        {
            get => fileCache;
            set => Set(ref fileCache, value);
        }
        private bool highlightSameWord;
        /// <summary>
        /// 开启高亮显示相同单词
        /// </summary>
        public bool HighlightSameWord
        {
            get => highlightSameWord;
            set => Set(ref highlightSameWord, value);
        }
        private bool tagAutoClose;
        /// <summary>
        /// 开启标签自动闭合
        /// </summary>
        public bool TagAutoClose
        {
            get => tagAutoClose;
            set => Set(ref tagAutoClose, value);
        }
        private bool autoSaveFile;
        /// <summary>
        /// 自动保存文件
        /// </summary>
        public bool AutoSaveFile
        {
            get => autoSaveFile;
            set => Set(ref autoSaveFile, value);
        }
    }
}
