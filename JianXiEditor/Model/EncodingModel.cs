using HandyControl.Tools;
using System.Collections.Generic;

namespace JianXiEditor.Model
{
    class EncodingModel : BindablePropertyBase
    {
        /// <summary>
        /// 编码格式列表
        /// </summary>
        public List<string> EncodingList { get; } = new List<string>()
        {
            "ASCII",
            "BIG5",
            "GB18030",
            "UTF-7",
            "UTF-8",
            "UTF-16",
            "UTF-16 BE",
            "UTF-16 LE",
            "UTF-32",
            "UTF-32 BE",
            "UTF-32 LE"
        };
        private string selectEncoding;
        /// <summary>
        /// 选择的编码格式
        /// </summary>
        public string SelectEncoding
        {
            get => selectEncoding;
            set => Set(ref selectEncoding, value);
        }
    }
}
