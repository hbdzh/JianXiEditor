// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System.IO;

namespace ICSharpCode.AvalonEdit.Highlighting
{
    static class Resources
    {
        static readonly string Prefix = typeof(Resources).FullName + ".";

        public static Stream OpenStream(string name)
        {
            Stream s = typeof(Resources).Assembly.GetManifestResourceStream(Prefix + name);
            if (s == null)
                throw new FileNotFoundException("The resource file '" + name + "' was not found.");
            return s;
        }

        internal static void RegisterBuiltInHighlightings(HighlightingManager.DefaultHighlightingManager hlm)
        {
            hlm.RegisterHighlighting("XmlDoc", null, "XmlDoc.xshd");
            hlm.RegisterHighlighting("C#", new[] { ".cs" }, "CSharp-Mode.xshd");

            hlm.RegisterHighlighting("JavaScript", new[] { ".js", ".ts" }, "JavaScript-Mode.xshd");
            hlm.RegisterHighlighting("HTML", new[] { ".html" }, "HTML-Mode.xshd");
            hlm.RegisterHighlighting("ASP/XHTML", new[] { ".aspx" }, "ASPX.xshd");
            hlm.RegisterHighlighting("Lua", new[] { ".lua" }, "Lua.xshd");
            hlm.RegisterHighlighting("Boo", new[] { ".boo" }, "Boo.xshd");
            hlm.RegisterHighlighting("Coco", new[] { ".atg" }, "Coco-Mode.xshd");
            hlm.RegisterHighlighting("CSS", new[] { ".css" }, "CSS-Mode.xshd");
            hlm.RegisterHighlighting("C++", new[] { ".c" }, "CPP-Mode.xshd");
            hlm.RegisterHighlighting("Java", new[] { ".java" }, "Java-Mode.xshd");
            hlm.RegisterHighlighting("Patch", new[] { ".patch" }, "Patch-Mode.xshd");
            hlm.RegisterHighlighting("PowerShell", new[] { ".bat" }, "PowerShell.xshd");
            hlm.RegisterHighlighting("PHP", new[] { ".php" }, "PHP-Mode.xshd");
            hlm.RegisterHighlighting("Python", new[] { ".py", ".pyw" }, "Python-Mode.xshd");
            hlm.RegisterHighlighting("TeX", new[] { ".tex" }, "Tex-Mode.xshd");
            hlm.RegisterHighlighting("TSQL", new[] { ".sql" }, "TSQL-Mode.xshd");
            hlm.RegisterHighlighting("VB", new[] { ".vb" }, "VB-Mode.xshd");
            hlm.RegisterHighlighting("XML", new[] { ".xml" }, "XML-Mode.xshd");
            //hlm.RegisterHighlighting("MarkDown", new[] { ".md" }, "MarkDown-Mode.xshd");//这个是文本框内不带样式，只有语法高亮
            hlm.RegisterHighlighting("MarkDownWithFontSize", new[] { ".md" }, "MarkDownWithFontSize-Mode.xshd");//带语法高亮，带样式
            hlm.RegisterHighlighting("Json", new[] { ".json" }, "Json.xshd");
            hlm.RegisterHighlighting("Sin", new[] { ".sin" }, "Sin.xshd");
            hlm.RegisterHighlighting("Ruby", new[] { ".rb" }, "Ruby.xshd");
        }
    }
}
