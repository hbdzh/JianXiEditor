using JianXiEditor.Config;
using System;
using System.IO;
using System.Text;

namespace JianXiEditor.Utility
{
    class FileReadWrite
    {
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">文件编码</param>
        /// <returns>读取到的内容</returns>
        public StringBuilder ReadFileContent(string filePath, string encoding)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                int _bufferSize = 16384;
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    using (StreamReader streamReader = new StreamReader(fileStream, Encoding.GetEncoding(encoding)))
                    {
                        char[] fileContents = new char[_bufferSize];
                        int charsRead = streamReader.Read(fileContents, 0, _bufferSize);
                        if (charsRead == 0)
                        {
                            stringBuilder.Append(string.Empty);
                            return stringBuilder;
                        }
                        while (charsRead > 0)
                        {
                            stringBuilder.Append(fileContents, 0, charsRead);
                            charsRead = streamReader.Read(fileContents, 0, _bufferSize);
                        }
                        fileStream.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                HandyControl.Tools.Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("读取文件时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
            return stringBuilder;
        }
        /// <summary>
        /// 写入文件内容
        /// </summary>
        /// <param name="filePath">保存路径</param>
        /// <param name="fileContent">保存内容</param>
        /// <param name="encoding">编码</param>
        public void WriteFileContent(string filePath, string fileContent, Encoding encoding)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                File.WriteAllText(filePath, fileContent, encoding);
            }
            catch (Exception ex)
            {
                HandyControl.Tools.Logger.Log(ex);
                HandyControl.Controls.Growl.Warning("写入文件时出现异常，已将异常原因记录到如下目录：" + Environment.NewLine + Config_Temp.AppDataPath_Log);
            }
        }
    }
}
