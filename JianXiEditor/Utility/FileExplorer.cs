using Microsoft.Win32;
using System.IO;

namespace JianXiEditor.Utility
{
    class FileExplorer
    {
        /// <summary>
        /// 打开文件的对话框
        /// </summary>
        /// <returns>打开对话框</returns>
        public static OpenFileDialog OpenFileDialogWindow()
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Title = "请选择要打开的文件",
                Filter = "所有文件(*.*)|*.*",
                RestoreDirectory = true
            };
            return ofd;
        }
        /// <summary>
        /// 保存文件的对话框
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>保存对话框</returns>
        public static SaveFileDialog SaveFileDialogWindow(string fileName)
        {
            FileExtension extensionConverter = new FileExtension
            {
                ExtensionConverter = Path.GetExtension(fileName)
            };
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Title = "请选择要保存的文件位置",
                FileName = fileName,
                Filter = "纯文件(*.txt; *.gitignore)|*.txt;*.gitignore|Batch(*.bat; *.cmd)|*.bat;*.cmd|C(*.c;*.h)|*.c;*.h|C++(*.cpp; *.cc; *.cxx; *.hpp; *.hh; *.hxx; *.ino)|*.cpp; *.cc; *.cxx; *.hpp; *.hh; *.hxx; *.ino|C#(*.cs)|*.cs|CSS(*.css)|*.css|Clojure(*.clj)|*.clj|Docker(*.dockerfile;*.containerfile)|*.dockerfile;*.containerfile|F#(*.fs;*.fsi;*.fsx)|*.fs;*.fsi;*.fsx|Go(*.go)|*.go|Groovy(*.groovy;*.gvy;*.gradle)|*.groovy;*.gvy;*.gradle|Graphql(*.graphql)|*.graphql|HTML(*.html; *.htm; *.shtml; *.xhtml;*.asp; *.aspx;*.svg)|*.html; *.htm; *.shtml; *.xhtml; *.asp; *.aspx;*.svg|HLSL(*.hsls;*.fx)|*.hsls;*.fx|InI(*.ini;*.inf)|*.ini;*.inf|Inno Setup Script(*.iss)|*.iss|Java(*.java;*.jsp)|*.java;*.jsp|JavaScript(*.js;*.es6)|*.js;*.es6|JSON(*.json)|*.json|Kotlin(*.kt)|*.kt|Less(*.less)|*.less|Lua(*.lua)|*.lua|Log(*.log)|*.log|MarkDown(*.md;*.markdown)|*.md;*.markdown|NSIS(*.nsi)|*.nsi|Objective-C(*.m;*.mm)|*.m;*.mm|Python(*.py;*.pyw)|*.py;*.pyw|PHP(*.php;*.php4;*.php5;*.phtml)|*.php;*.php4;*.php5;*.phtml|PowerShell(*.ps1;*.psm1;*.psd1)|*.ps1;*.psm1;*.psd1|Perl(*.pl;*.pm)|*.pl;*.pm|Perl6(*.p6;*.pl6;*.pm6)|*.p6;*.pl6;*.pm6|Properties(*.properties;*.cfg;*.conf)|*.properties;*.cfg;*.conf|R(*.r)|*.r|Razor(*.cshtml;*.razor)|*.cshtml;*.razor|Ruby(*.rb)|*.rb|Rust(*.rs)|*.rs|Sin(*.sin)|*.sin|SCSS(*.scss; *.sass)|*.scss;*.sass|SQL(*.sql;*.sqlite)|*.sql;*.sqlite|Swift(*.swift)|*.swift|Shell Script(*.sh;*.bash;*.install;*.profile)|*.sh;*.bash;*.install;*.profile|ShaderLab(*.shader)|*.shader|TypeScript(*.ts;*.tsx)|*.ts;*.tsx|Visual Basic(*.vb;*.vbs)|*.vb;*.vbs|Vue(*.vue)|*.vue|XML(*.xml;*.xsd;*.axml;*.xaml;*.pubxml;*.xshd;*.csproj;*.csproj.user;*.resx;*.settings;*.iml)|*.xml;*.xsd;*.svg;*.axml;*.xaml;*.pubxml;*.xshd;*.csproj;*.csproj.user;*.resx;*.settings;*.iml|XSL(*.xsl;*.xslt)|*.xsl;*.xslt|YAML(*.yml;*.eyaml)|*.yml;*.eyaml|无拓展(*.)|*.|所有文件(*.*)|*.*",
                FilterIndex = (int)extensionConverter.ExtensionConverter,
                AddExtension = true,
                RestoreDirectory = true//恢复之前选择的目录
            };
            return sfd;
        }
    }
}
