using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using JianXiEditor.Model;
using System.Collections.Generic;
using System.Windows.Input;

namespace JianXiEditor.Service.EditorPlus
{
    class CommentHandle
    {
        /// <summary>
        /// 插入注释
        /// </summary>
        /// <param name="main">主界面视图模型</param>
        /// <param name="textEditor">编辑器</param>
        public void InsertOrRemoveComment(MainModel main, TextEditor textEditor)
        {
            if (textEditor.SelectionLength > 0)//选中了内容
            {
                switch (main.FileSyntaxHighlighting)
                {
                    case "C#":
                    case "C/C++":
                    case "Java":
                    case "JavaScript":
                    case "PHP":
                        GeneralComment(textEditor, "//", "/*", "*/");
                        break;
                    case "Python":
                        PythonComment(textEditor);
                        break;
                    case "HTML":
                    case "XML":
                        WebComment(textEditor, "<!--", "-->");
                        break;
                    case "CSS":
                        WebComment(textEditor, "/*", "*/");
                        break;
                    case "Ruby":
                        GeneralComment(textEditor, "#", "=begin", "=end");
                        break;
                    case "SQL":
                        SqlComment(textEditor);
                        break;
                    case "PowerShell":
                        GeneralComment(textEditor, "#", "<#", "#>");
                        break;
                    case "Lua":
                        GeneralComment(textEditor, "--", "--[[", "---]]");
                        break;
                    case "VB":
                        VBComment(textEditor);
                        break;
                    case "ASPX":
                        WebComment(textEditor, "<!--", "-->");
                        WebComment(textEditor, "<%--", "--%>");
                        break;
                }
            }
        }

        /// <summary>
        /// CSS或Html、Xml的注释
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        /// <param name="beforeComment">前注释</param>
        /// <param name="afterComment">后注释</param>
        private void WebComment(TextEditor textEditor, string beforeComment, string afterComment)
        {
            string select = textEditor.SelectedText;
            IEnumerable<SelectionSegment> segments = textEditor.TextArea.Selection.Segments;
            if (select.Contains(beforeComment) && select.Contains(afterComment))//选中的可能是个多行注释，这个时候就需要取消注释
            {
                select = select.Replace(beforeComment, "").Replace(afterComment, "");
                foreach (SelectionSegment item in segments)
                {
                    textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, select);
                }
            }
            else//这才是插入注释
            {
                //因为已经提前判断了是单行，所以不会出现遍历好几个结果的情况
                foreach (SelectionSegment item in segments)
                {
                    textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, $"{beforeComment} {select} {afterComment}");//注意！别少了空格
                }
            }
        }

        /// <summary>
        /// Python注释
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        private void PythonComment(TextEditor textEditor)
        {
            string select = textEditor.SelectedText;
            IEnumerable<SelectionSegment> segments = textEditor.TextArea.Selection.Segments;
            if (select.Contains("'''"))//选中的可能是个多行注释，这个时候就需要取消注释
            {
                select = select.Replace("'''", "");
                foreach (SelectionSegment item in segments)
                {
                    textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, select);
                }
            }
            else if (select.Contains("\"\"\""))//选中的可能是个多行注释，这个时候就需要取消注释
            {
                select = select.Replace("\"\"\"", "");
                foreach (SelectionSegment item in segments)
                {
                    textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, select);
                }
            }
            else if (select.Contains("#"))//选中的是单行注释，这个时候就需要取消注释
            {
                select = select.Replace("#", "");
                foreach (SelectionSegment item in segments)
                {
                    textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, select);
                }
            }
            else//这才是插入注释
            {
                if (textEditor.TextArea.Selection.IsMultiline == true)//选中的是多行
                {
                    foreach (SelectionSegment item in segments)
                    {
                        textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, "'''" + select + "'''");
                    }
                }
                else//选择的是单行
                {
                    //因为已经提前判断了是单行，所以不会出现遍历好几个结果的情况
                    foreach (SelectionSegment item in segments)
                    {
                        textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, "#" + select);
                    }
                }
            }
        }

        /// <summary>
        /// Sql注释
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        private void SqlComment(TextEditor textEditor)
        {
            string select = textEditor.SelectedText;
            IEnumerable<SelectionSegment> segments = textEditor.TextArea.Selection.Segments;
            if (select.Contains("/*") && select.Contains("*/"))//选中的可能是个多行注释，这个时候就需要取消注释
            {
                select = select.Replace("/*", "").Replace("*/", "");
                foreach (SelectionSegment item in segments)
                {
                    textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, select);
                }
            }
            else if (select.Contains("--"))//选中的是单行注释，这个时候就需要取消注释
            {
                select = select.Replace("--", "");
                foreach (SelectionSegment item in segments)
                {
                    textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, select);
                }
            }
            else if (select.Contains("#"))//选中的是单行注释，这个时候就需要取消注释
            {
                select = select.Replace("#", "");
                foreach (SelectionSegment item in segments)
                {
                    textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, select);
                }
            }
            else//这才是插入注释
            {
                if (textEditor.TextArea.Selection.IsMultiline == true)//选中的是多行
                {
                    foreach (SelectionSegment item in segments)
                    {
                        textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, "/* " + select + " */");
                    }
                }
                else//选择的是单行
                {
                    //因为已经提前判断了是单行，所以不会出现遍历好几个结果的情况
                    foreach (SelectionSegment item in segments)
                    {
                        textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, "#" + select);
                    }
                }
            }
        }

        /// <summary>
        /// C系、Ruby、PowerShell、Lua注释
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        private void GeneralComment(TextEditor textEditor, string lineComment, string beforeComment, string afterComment)
        {
            string select = textEditor.SelectedText;
            IEnumerable<SelectionSegment> segments = textEditor.TextArea.Selection.Segments;
            if (select.Contains(beforeComment) && select.Contains(afterComment))//选中的可能是个多行注释，这个时候就需要取消注释
            {
                select = select.Replace(beforeComment, "").Replace(afterComment, "");
                foreach (SelectionSegment item in segments)
                {
                    textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, select);
                }
            }
            else if (select.Contains(lineComment))//选中的是单行注释，这个时候就需要取消注释
            {
                select = select.Replace(lineComment, "");
                foreach (SelectionSegment item in segments)
                {
                    textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, select);
                }
            }
            else//这才是插入注释
            {
                if (textEditor.TextArea.Selection.IsMultiline == true)//选中的是多行
                {
                    foreach (SelectionSegment item in segments)
                    {
                        textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, beforeComment + " " + select + " " + afterComment);
                    }
                }
                else//选择的是单行
                {
                    //因为已经提前判断了是单行，所以不会出现遍历好几个结果的情况
                    foreach (SelectionSegment item in segments)
                    {
                        textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, lineComment + select);
                    }
                }
            }
        }

        /// <summary>
        /// VS注释
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        private void VBComment(TextEditor textEditor)
        {
            string select = textEditor.SelectedText;
            IEnumerable<SelectionSegment> segments = textEditor.TextArea.Selection.Segments;
            if (select.Contains("'"))//选中的可能是个多行注释，这个时候就需要取消注释
            {
                select = select.Replace("'", "");
                foreach (SelectionSegment item in segments)
                {
                    textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, select);
                }
            }
            else if (select.Contains("rem"))//选中的是单行注释，这个时候就需要取消注释
            {
                select = select.Replace("rem", "");
                foreach (SelectionSegment item in segments)
                {
                    textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, select);
                }
            }
            else//这才是插入注释
            {
                //因为已经提前判断了是单行，所以不会出现遍历好几个结果的情况
                foreach (SelectionSegment item in segments)
                {
                    textEditor.Document.Replace(item.StartOffset, textEditor.SelectionLength, "'" + select);
                }
            }
        }

        /// <summary>
        /// C#三行注释
        /// </summary>
        /// <param name="main">主界面模型</param>
        /// <param name="textEditor">编辑器</param>
        /// <param name="e">事件参数</param>
        public void CSharpThereComment(MainModel main, TextEditor textEditor, TextCompositionEventArgs e)
        {
            int caretOffset = textEditor.CaretOffset;//获取当前光标位置
            if (e.Text == "/" && caretOffset >= 2)
            {
                if (main.FileSyntaxHighlighting == "C#")
                {
                    string beforeText = textEditor.Document.GetText(caretOffset - 1, 1);
                    string beforebBeforeText = textEditor.Document.GetText(caretOffset - 2, 1);
                    if (beforeText == "/" && beforebBeforeText == "/")
                    {
                        textEditor.Document.Insert(caretOffset, "/ <summary>\r\n/// \r\n/// </summary>");
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
