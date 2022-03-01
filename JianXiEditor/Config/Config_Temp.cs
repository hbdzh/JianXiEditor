using System;

namespace JianXiEditor.Config
{
    /// <summary>
    /// 临时配置
    /// </summary>
    class Config_Temp
    {
        /// <summary>
        /// App数据根目录
        /// </summary>
        public static string AppDataPath { get; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\JianXiEditor\\";
        /// <summary>
        /// App更新文件夹
        /// </summary>
        public static string AppDataPath_Update { get; } = $"{AppDataPath}Update\\";
        /// <summary>
        /// App配置文件夹
        /// </summary>
        public static string AppDataPath_Config { get; } = $"{AppDataPath}Config\\";
        /// <summary>
        /// App历史文件夹
        /// </summary>
        public static string AppDataPath_History { get; } = $"{AppDataPath}History\\";
        /// <summary>
        /// App备份文件夹
        /// </summary>
        public static string AppDataPath_BackUp { get; } = $"{AppDataPath}BackUp\\";
        /// <summary>
        /// App日志文件夹
        /// </summary>
        public static string AppDataPath_Log { get; } = $"{AppDataPath}Log\\";
        /// <summary>
        /// App所在目录
        /// </summary>
        public static string AppPath { get; } = $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}\\";
        /// <summary>
        /// 文件内容
        /// </summary>
        public static string FileContent { get; set; } = null;
        /// <summary>
        /// 根据系统位数设置更新时要下载的文件名
        /// </summary>
        public static string AppSetupName { get; set; } = Environment.Is64BitProcess == true ? "JianXiEditor_Setup_X64.exe" : "JianXiEditor_Setup_X32.exe";
    }
}
