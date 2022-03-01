using HandyControl.Tools;
using System.Collections.Generic;

namespace JianXiEditor.Model
{
    class LineBreakModel : BindablePropertyBase
    {
        /// <summary>
        /// 换行符列表
        /// </summary>
        public List<string> LineBreakList { get; } = new List<string>()
        {
            "CRLF",
            "CR",
            "LF"
        };
        private string selectLineBreak;
        /// <summary>
        /// 选择的换行符
        /// </summary>
        public string SelectLineBreak
        {
            get => selectLineBreak;
            set => Set(ref selectLineBreak, value);
        }
    }
}
