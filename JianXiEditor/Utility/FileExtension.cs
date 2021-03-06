namespace JianXiEditor.Utility
{
    class FileExtension
    {
        private object extensionConverter;
        /// <summary>
        /// 文件拓展名转换
        /// </summary>
        public object ExtensionConverter
        {
            get
            {
                switch (extensionConverter)
                {
                    case ".txt":
                        return 1;
                    case ".bat":
                    case ".cmd":
                        return 2;
                    case ".c":
                    case ".h":
                        return 3;
                    case ".cpp":
                    case ".cc":
                    case ".cxx":
                    case ".hpp":
                    case ".hh":
                    case ".hxx":
                    case ".ino":
                        return 4;
                    case ".cs":
                        return 5;
                    case ".css":
                        return 6;
                    case ".clj":
                        return 7;
                    case ".dockerfile":
                    case ".containerfile":
                        return 8;
                    case ".fs":
                    case ".fsi":
                    case ".fsx":
                        return 9;
                    case ".go":
                        return 10;
                    case ".groovy":
                    case ".gvy":
                    case ".gradle":
                        return 11;
                    case ".graphql":
                        return 12;
                    case ".html":
                    case ".htm":
                    case ".shtml":
                    case ".xhtml":
                    case ".asp":
                    case ".aspx":
                    case ".svg":
                        return 13;
                    case ".hsls":
                    case ".fx":
                        return 14;
                    case ".ini":
                    case ".inf":
                        return 15;
                    case ".iss":
                        return 16;
                    case ".java":
                    case ".jsp":
                        return 17;
                    case ".js":
                    case ".es6":
                        return 18;
                    case ".json":
                        return 19;
                    case ".kt":
                        return 20;
                    case ".less":
                        return 21;
                    case ".lua":
                        return 22;
                    case ".log":
                        return 23;
                    case ".md":
                    case ".markdown":
                        return 24;
                    case ".nsi":
                        return 25;
                    case ".m":
                    case ".mm":
                        return 26;
                    case ".py":
                    case ".pyw":
                        return 27;
                    case ".php":
                    case ".php4":
                    case ".php5":
                    case ".phtml":
                        return 28;
                    case ".ps1":
                    case ".psm1":
                    case ".psd1":
                        return 29;
                    case ".pl":
                    case ".pm":
                        return 30;
                    case ".p6":
                    case ".pl6":
                    case ".pm6":
                        return 31;
                    case ".properties":
                    case ".cfg":
                    case ".conf":
                        return 32;
                    case ".r":
                        return 33;
                    case ".cshtml":
                    case ".razor":
                        return 34;
                    case ".rb":
                        return 35;
                    case ".rs":
                        return 36;
                    case ".sin":
                        return 37;
                    case ".scss":
                        return 38;
                    case ".sql":
                    case ".sqlite":
                        return 39;
                    case ".swift":
                        return 40;
                    case ".sh":
                    case ".bash":
                    case ".install":
                    case ".profile":
                        return 41;
                    case ".shader":
                        return 42;
                    case ".ts":
                    case ".tsx":
                        return 43;
                    case ".vb":
                    case ".vbs":
                        return 44;
                    case ".vue":
                        return 45;
                    case ".xml":
                    case ".xsd":
                    case ".axml":
                    case ".xaml":
                    case ".pubxml":
                    case ".xshd":
                    case ".csproj":
                    case ".csproj.user":
                    case ".resx":
                    case ".settings":
                    case ".iml":
                        return 46;
                    case ".xsl":
                    case ".xslt":
                        return 47;
                    case ".yml":
                    case ".eyaml":
                        return 48;
                    case "":
                        return 49;
                    default:
                        return 50;
                }
            }
            set { extensionConverter = value; }
        }
        /// <summary>
        /// 高亮语法转换（拓展名转名称）
        /// </summary>
        /// <param name="languageExtension">语言拓展名</param>
        /// <returns>语言名称</returns>
        public static string ToName(string languageExtension)
        {
            switch (languageExtension)
            {
                case ".bat":
                case ".cmd":
                case ".ps1":
                case ".psm1":
                case ".psd1":
                case ".properties":
                case ".cfg":
                case ".conf":
                case ".sh":
                case ".bash":
                case ".install":
                case ".profile":
                case ".shader":
                case ".nsi":
                case ".ini":
                case ".inf":
                case ".iss":
                case ".graphql":
                case ".log":
                    return "PowerShell";
                case ".c":
                case ".h":
                case ".cpp":
                case ".cc":
                case ".cxx":
                case ".hpp":
                case ".hh":
                case ".hxx":
                case ".ino":
                case ".m":
                case ".mm":
                case ".r":
                    return "C/C++";
                case ".cs":
                    return "C#";
                case ".css":
                case ".sass":
                case ".less":
                case ".scss":
                    return "CSS";
                case ".clj":
                case ".java":
                case ".jsp":
                case ".kt":
                case ".swift":
                    return "Java";
                case ".groovy":
                case ".gvy":
                case ".gradle":
                case ".js":
                case ".es6":
                case ".ts":
                case ".tsx":
                case ".vue":
                    return "JavaScript";
                case ".json":
                    return "Json";
                case ".py":
                case ".pyw":
                    return "Python";
                case ".php":
                case ".php4":
                case ".php5":
                case ".phtml":
                case ".pl":
                case ".pm":
                case ".p6":
                case ".pl6":
                case ".pm6":
                case ".rs":
                    return "PHP";
                case ".rb":
                    return "Ruby";
                case ".sql":
                case ".sqlite":
                    return "SQL";
                case ".vb":
                case ".vbs":
                    return "VB";
                case ".html":
                case ".htm":
                case ".shtml":
                case ".xhtml":
                case ".asp":
                case ".aspx":
                case ".svg":
                case ".hsls":
                case ".fx":
                case ".dockerfile":
                case ".containerfile":
                case ".cshtml":
                case ".razor":
                    return "HTML";
                case ".xml":
                case ".xsd":
                case ".axml":
                case ".xaml":
                case ".pubxml":
                case ".csproj":
                case ".user":
                case ".iml":
                case ".xsl":
                case ".xslt":
                case ".yml":
                case ".eyaml":
                case ".xshd":
                case ".resx":
                case ".settings":
                    return "XML";
                case ".lua":
                    return "Lua";
                case ".md":
                case ".markdown":
                    return "Markdown";
                case ".fs":
                case ".fsi":
                case ".fsx":
                case ".go":
                    return "Text";
                case ".sin":
                    return "Sin";
                default:
                    return "Text";
            }
        }
        /// <summary>
        /// 高亮语法转换（名称转拓展名）
        /// </summary>
        /// <param name="languageName">语言名称</param>
        /// <returns></returns>
        public static string ToExtension(string languageName)
        {
            switch (languageName)
            {
                case "PowerShell":
                    return ".bat";
                case "C/C++":
                    return ".c";
                case "C#":
                    return ".cs";
                case "CSS":
                    return ".css";
                case "Java":
                    return ".java";
                case "JavaScript":
                    return ".js";
                case "Json":
                    return ".json";
                case "Python":
                    return ".py";
                case "PHP":
                    return ".php";
                case "Ruby":
                    return ".rb";
                case "SQL":
                    return ".sql";
                case "VB":
                    return ".vb";
                case "HTML":
                    return ".html";
                case "XML":
                    return ".xml";
                case "Lua":
                    return ".lua";
                case "Markdown":
                    return ".md";
                case "Sin":
                    return ".sin";
                default:
                    return ".txt";
            }
        }
    }
}
