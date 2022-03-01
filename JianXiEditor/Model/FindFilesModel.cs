using HandyControl.Tools;
using System.Collections.Generic;

namespace JianXiEditor.Model
{
    class FindFilesModel : BindablePropertyBase
    {
        private List<FindFilesModel> fileList;
        /// <summary>
        /// 文件路径列表
        /// </summary>
        public List<FindFilesModel> FileList
        {
            get => fileList;
            set => Set(ref fileList, value);
        }
        private string filePath;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            get => filePath;
            set => Set(ref filePath, value);
        }
        private string fileContent;
        /// <summary>
        /// 查找的内容
        /// </summary>
        public string FileContent
        {
            get => fileContent;
            set => Set(ref fileContent, value);
        }
        private string fileLine;
        /// <summary>
        /// 所在行
        /// </summary>
        public string FileLine
        {
            get => fileLine;
            set => Set(ref fileLine, value);
        }
    }
}
