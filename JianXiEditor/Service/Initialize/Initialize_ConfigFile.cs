using JianXiEditor.Config;
using MiniConfigure;
using System;
using System.IO;

namespace JianXiEditor.Service.Initialize
{
    /// <summary>
    /// 初始化配置文件
    /// </summary>
    class Initialize_ConfigFile
    {
        /// <summary>
        /// 如果存在配置文件则读取，不存在则先初始化一个
        /// </summary>
        public void Initialize_ConfigFileMain()
        {
            if (!File.Exists(Config_Temp.AppDataPath_Config + "App.sin"))
                Initialize_ConfigFile_App();
            if (!File.Exists(Config_Temp.AppDataPath_Config + "Editor.sin"))
                Initialize_ConfigFile_Editor();
            if (!File.Exists(Config_Temp.AppDataPath_BackUp))
                Directory.CreateDirectory(Config_Temp.AppDataPath_BackUp);
            if (!File.Exists(Config_Temp.AppDataPath_Log))
                Directory.CreateDirectory(Config_Temp.AppDataPath_Log);
        }
        /// <summary>
        /// 初始化程序配置文件
        /// </summary>
        private void Initialize_ConfigFile_App()
        {
            if (!Directory.Exists(Config_Temp.AppDataPath_Config))//如果不存在配置文件夹，则新建一个
                Directory.CreateDirectory(Config_Temp.AppDataPath_Config);
            //App.sin
            try
            {
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "AutoUpdate", "True");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "Theme", "Light");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "EditorLocation", "Left");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FastMoveText", "False");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FastMoveLine", "False");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "QuickCopyLine", "False");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "RealDisplayState", "True");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "CloseBackUpFile", "False");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "HighlightSameWord", "False");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FontFamily", "Consolas");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "TagAutoClose", "True");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "App.sin", "Config", "FileCache", "False");
            }
            catch (Exception ex)
            {
                HandyControl.Tools.Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("初始化程序配置文件时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        }
        /// <summary>
        /// 初始化编辑器配置文件
        /// </summary>
        private void Initialize_ConfigFile_Editor()
        {
            //false就是新建一个文件，并添加进去内容
            //true就是不新建，直接添加
            try
            {
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Font", "FontSize", "16");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowLineNumbers", "True");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "WordWarp", "True");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "VirtualSpace", "False");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "HighlightCurrentLine", "False");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ConvertTabsToSpaces", "True");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "EnableEmailHyperlinks", "False");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "EnableHyperlinks", "False");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowEndOfLine", "False");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowSpaces", "False");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "AllowScrollBelowDocument", "True");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "CutCopyWholeLine", "False");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowTabs", "False");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "LineHeight", "1.2");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "IndentationSize", "4");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "HideCursorWhileTyping", "True");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ShowColumnRuler", "False");
                MiniConfig.AppendConfig(Config_Temp.AppDataPath_Config + "Editor.sin", "Paragraph", "ColumnRulerPosition", "10");
            }
            catch (Exception ex)
            {
                HandyControl.Tools.Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("初始化编辑器配置文件时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        }
    }
}
