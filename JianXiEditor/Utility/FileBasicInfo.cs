using System.IO;
using System.Text;
using UtfUnknown;

namespace JianXiEditor.Utility
{
    /// <summary>
    /// 文件基本信息
    /// </summary>
    class FileBasicInfo
    {
        /// <summary>
        /// 获取编码格式名称
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>编码名称</returns>
        public static string GetEncodingName(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return "UTF-8";
            }
            else
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    DetectionResult result = CharsetDetector.DetectFromFile(filePath);
                    ///获得最佳的Detection 
                    DetectionDetail resultDetected = result.Detected;
                    switch (resultDetected)
                    {
                        case null:
                            return "UTF-8";
                        default:
                            return resultDetected.EncodingName.ToUpper();
                    }
                }
            }
        }
        /// <summary>
        /// 获取编码格式
        /// </summary>
        /// <param name="encodingName">编码名称</param>
        /// <returns>编码格式</returns>
        public static Encoding GetEncoding(string encodingName)
        {
            switch (encodingName)
            {
                case "ASCII":
                    return Encoding.ASCII;
                case "UTF-7":
                    return new UTF7Encoding();
                case "UTF-8":
                    return new UTF8Encoding(false);
                case "UTF-16 BE":
                    return new UnicodeEncoding(true, false);
                case "UTF-16 LE":
                    return new UnicodeEncoding(false, false);
                case "UTF-32 BE":
                    return new UTF32Encoding(true, false);
                case "UTF-32 LE":
                    return new UTF32Encoding(false, false);
                default:
                    return Encoding.GetEncoding(encodingName);
            }
        }
        /// <summary>
        /// 文件换行符格式
        /// </summary>
        /// <param name="fileContent">文件内容</param>
        /// <returns>换行符格式</returns>
        public static string GetLineBreak(string fileContent)
        {
            if (fileContent.Contains("\r\n"))
            {
                return "CRLF";
            }
            else if (fileContent.Contains("\r"))
            {
                return "CR";
            }
            else if (fileContent.Contains("\n"))
            {
                return "LF";
            }
            else
            {
                return "CRLF";
            }
        }
        /// <summary>
        /// 根据换行符名字，获取换行符格式
        /// </summary>
        /// <param name="name">换行符名字</param>
        /// <returns>换行符格式</returns>
        public static string GetLineBreakName(string name)
        {
            switch (name)
            {
                case "CRLF":
                    return "\r\n";
                case "CR":
                    return "\r";
                case "LF":
                    return "\n";
                default:
                    return "";
            }
        }
    }
}
