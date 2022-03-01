using MiniConfigure;

namespace JianXiEditor.Config
{
    /// <summary>
    /// 程序设置项
    /// </summary>
    class Config_App
    {
        /// <summary>
        /// 用户配置-主题
        /// </summary>
        public static string Config_UseTheme
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "Theme")))//如果theme这个元素不存在在文件中
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "Theme", "Light");//就添加一个进去
                }
                return MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "Theme");
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "Theme", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "Theme"), value.ToString());
            }
        }

        /// <summary>
        /// 用户配置-开启自动更新
        /// </summary>
        public static bool Config_AutoUpdate
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "AutoUpdate")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "AutoUpdate", "True");//就添加一个进去
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "AutoUpdate"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "AutoUpdate", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "AutoUpdate"), value.ToString());
            }
        }

        /// <summary>
        /// 用户配置-开启快速移动文字功能
        /// </summary>
        public static bool Config_FastMoveText
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FastMoveText")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FastMoveText", "False");//就添加一个进去
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FastMoveText"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FastMoveText", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FastMoveText"), value.ToString());
            }
        }

        /// <summary>
        /// 用户配置-开启快速移动行功能
        /// </summary>
        public static bool Config_FastMoveLine
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FastMoveLine")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FastMoveLine", "False");//就添加一个进去
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FastMoveLine"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FastMoveLine", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FastMoveLine"), value.ToString());
            }
        }

        /// <summary>
        /// 用户配置-开启快速复制当前行功能
        /// </summary>
        public static bool Config_QuickCopyLine
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "QuickCopyLine")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "QuickCopyLine", "False");//就添加一个进去
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "QuickCopyLine"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "QuickCopyLine", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "QuickCopyLine"), value.ToString());
            }
        }

        /// <summary>
        /// 用户配置-开启实时显示文件状态功能
        /// </summary>
        public static bool Config_RealDisplayState
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "RealDisplayState")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "RealDisplayState", "True");//就添加一个进去
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "RealDisplayState"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "RealDisplayState", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "RealDisplayState"), value.ToString());
            }
        }

        /// <summary>
        /// 用户配置-文件缓存
        /// </summary>
        public static bool Config_FileCache
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FileCache")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FileCache", "True");//就添加一个进去
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FileCache"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FileCache", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FileCache"), value.ToString());
            }
        }

        /// <summary>
        /// 用户配置-编辑器位置
        /// </summary>
        public static string Config_EditorLocation
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "EditorLocation")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "EditorLocation", "Left");//就添加一个进去
                }
                return MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "EditorLocation");
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "EditorLocation", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "EditorLocation"), value.ToString());
            }
        }

        /// <summary>
        /// 用户配置-单词高亮显示
        /// </summary>
        public static bool Config_HighlightSameWord
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "HighlightSameWord")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "HighlightSameWord", "True");//就添加一个进去
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "HighlightSameWord"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "HighlightSameWord", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "HighlightSameWord"), value.ToString());
            }
        }

        /// <summary>
        /// 用户配置-字体
        /// </summary>
        public static string Config_FontFamily
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FontFamily")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FontFamily", "Consolas");//就添加一个进去
                }
                return MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FontFamily");
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FontFamily", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FontFamily"), value.ToString());
            }
        }

        /// <summary>
        /// 用户配置-标签自动闭合
        /// </summary>
        public static bool Config_TagAutoClose
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "TagAutoClose")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "TagAutoClose", "True");//就添加一个进去
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "TagAutoClose"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "TagAutoClose", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "TagAutoClose"), value.ToString());
            }
        }
        /// <summary>
        /// 用户配置-文件自动保存
        /// </summary>
        public static bool Config_AutoSaveFile
        {
            get
            {
                if (string.IsNullOrEmpty(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "AutoSaveFile")))
                {
                    MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "AutoSaveFile", "True");//就添加一个进去
                }
                return bool.Parse(MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "AutoSaveFile"));
            }
            set
            {
                MiniValue.SetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "AutoSaveFile", MiniValue.GetValue(Config_Temp.AppDataPath_Config + "App.sin", "Config", "AutoSaveFile"), value.ToString());
            }
        }
    }
}
