using MiniConfigure;
namespace JianXiEditor.Config
{
    /// <summary>
    /// 编辑器设置项
    /// </summary>
    class Config_Editor
    {
        /// <summary>
        /// 用户配置-字体大小
        /// </summary>
        public static double Config_FontSize
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Font", "FontSize")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Font", "FontSize", "18");//就添加一个进去
                }
                string s = MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Font", "FontSize");
                return double.Parse(s);
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Font", "FontSize", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Font", "FontSize"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-显示行号
        /// </summary>
        public static bool Config_ShowLineNumbers
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowLineNumbers")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowLineNumbers", "True");//就添加一个进去
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowLineNumbers"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowLineNumbers", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowLineNumbers"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-自动换行
        /// </summary>
        public static bool Config_WordWarp
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "WordWarp")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "WordWarp", "True");//就添加一个进去
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "WordWarp"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "WordWarp", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "WordWarp"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-虚拟空格
        /// </summary>
        public static bool Config_VirtualSpace
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "VirtualSpace")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "VirtualSpace", "False");//就添加一个进去
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "VirtualSpace"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "VirtualSpace", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "VirtualSpace"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-高亮当前行
        /// </summary>
        public static bool Config_HighlightCurrentLine
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "HighlightCurrentLine")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "HighlightCurrentLine", "False");
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "HighlightCurrentLine"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "HighlightCurrentLine", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "HighlightCurrentLine"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-将Tab转为空格
        /// </summary>
        public static bool Config_ConvertTabsToSpaces
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ConvertTabsToSpaces")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ConvertTabsToSpaces", "True");
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ConvertTabsToSpaces"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ConvertTabsToSpaces", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ConvertTabsToSpaces"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-邮箱显示为超链接
        /// </summary>
        public static bool Config_EnableEmailHyperlinks
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "EnableEmailHyperlinks")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "EnableEmailHyperlinks", "False");
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "EnableEmailHyperlinks"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "EnableEmailHyperlinks", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "EnableEmailHyperlinks"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-超链接是否可点击
        /// </summary>
        public static bool Config_EnableHyperlinks
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "EnableHyperlinks")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "EnableHyperlinks", "False");
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "EnableHyperlinks"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "EnableHyperlinks", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "EnableHyperlinks"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-显示换行符
        /// </summary>
        public static bool Config_ShowEndOfLine
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowEndOfLine")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowEndOfLine", "False");
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowEndOfLine"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowEndOfLine", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowEndOfLine"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-显示空格
        /// </summary>
        public static bool Config_ShowSpaces
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowSpaces")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowSpaces", "False");//就添加一个进去
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowSpaces"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowSpaces", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowSpaces"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-允许滚动到文档底部
        /// </summary>
        public static bool Config_AllowScrollBelowDocument
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "AllowScrollBelowDocument")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "AllowScrollBelowDocument", "True");
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "AllowScrollBelowDocument"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "AllowScrollBelowDocument", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "AllowScrollBelowDocument"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-当没有选中内容时，复制整个行
        /// </summary>
        public static bool Config_CutCopyWholeLine
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "CutCopyWholeLine")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "CutCopyWholeLine", "False");
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "CutCopyWholeLine"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "CutCopyWholeLine", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "CutCopyWholeLine"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-显示Tab符号
        /// </summary>
        public static bool Config_ShowTabs
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowTabs")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowTabs", "False");
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowTabs"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowTabs", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowTabs"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-行距（行高）
        /// </summary>
        public static double Config_LineHeight
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "LineHeight")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "LineHeight", "1.2");//就添加一个进去
                }
                return double.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "LineHeight"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "LineHeight", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "LineHeight"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-缩进宽度
        /// </summary>
        public static int Config_IndentationSize
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "IndentationSize")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "IndentationSize", "4");//就添加一个进去
                }
                return int.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "IndentationSize"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "IndentationSize", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "IndentationSize"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-输入时隐藏光标
        /// </summary>
        public static bool Config_HideCursorWhileTyping
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "HideCursorWhileTyping")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "HideCursorWhileTyping ", "True");
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "HideCursorWhileTyping"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "HideCursorWhileTyping ", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "HideCursorWhileTyping"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-显示标尺
        /// </summary>
        public static bool Config_ShowColumnRuler
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowColumnRuler")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowColumnRuler ", "False");
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowColumnRuler"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowColumnRuler ", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowColumnRuler"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-标尺位置
        /// </summary>
        public static int Config_ColumnRulerPosition
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ColumnRulerPosition")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ColumnRulerPosition", "10");//就添加一个进去
                }
                return int.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ColumnRulerPosition"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ColumnRulerPosition", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ColumnRulerPosition"), value.ToString());
            }
        }
    }
}
