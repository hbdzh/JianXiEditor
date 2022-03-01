using HandyControl.Tools;
using System.Collections.Generic;

namespace JianXiEditor.Model
{
    class SyntaxHighlightingModel : BindablePropertyBase
    {
        /// <summary>
        /// 程序语言列表
        /// </summary>
        public List<string> ProgramLanguages { get; set; } = new List<string>
        {
            "C/C++",
            "C#",
            "Java",
            "HTML",
            "CSS",
            "JavaScript",
            "Sin",
            "Python",
            "PHP",
            "Ruby",
            "SQL",
            "XML",
            "Json",
            "PowerShell",
            "Lua",
            "VB",
            "ASPX",
            "Markdown",
            "Text"
        };
        private string selectProgramLanguage;
        /// <summary>
        /// 选择的语言
        /// </summary>
        public string SelectProgramLanguage
        {
            get => selectProgramLanguage;
            set => Set(ref selectProgramLanguage, value);
        }
    }
}
