using ICSharpCode.AvalonEdit;
using JianXiEditor.Model;
using System.Windows.Input;

namespace JianXiEditor.Service.EditorPlus
{
    /// <summary>
    /// 标签闭合处理
    /// </summary>
    class TagHandle
    {
        private static string lastInput = null;//上一次输入的内容
        /// <summary>
        /// 标签自动闭合
        /// </summary>
        /// <param name="setting">设置模型</param>
        /// <param name="textEditor">编辑器</param>
        /// <param name="e">事件参数</param>
        public void TagAutoClose(SettingModel setting, TextEditor textEditor, TextCompositionEventArgs e)
        {
            int currentOffset = textEditor.TextArea.Caret.Offset;
            if (setting.TagAutoClose == true)
            {
                switch (e.Text)
                {
                    case ")":
                        if (lastInput == "(")
                        {
                            textEditor.Document.Remove(currentOffset, 1);
                            lastInput = null;
                        }
                        break;
                    case "}":
                        if (lastInput == "{")
                        {
                            textEditor.Document.Remove(currentOffset, 1);
                            lastInput = null;
                        }
                        break;
                    case ">":
                        if (lastInput == "<")
                        {
                            textEditor.Document.Remove(currentOffset, 1);
                            lastInput = null;
                        }
                        break;
                    case "]":
                        if (lastInput == "[")
                        {
                            textEditor.Document.Remove(currentOffset, 1);
                            lastInput = null;
                        }
                        break;
                    case "】":
                        if (lastInput == "【")
                        {
                            textEditor.Document.Remove(currentOffset, 1);
                            lastInput = null;
                        }
                        break;
                    case "）":
                        if (lastInput == "（")
                        {
                            textEditor.Document.Remove(currentOffset, 1);
                            lastInput = null;
                        }
                        break;
                    case "》":
                        if (lastInput == "《")
                        {
                            textEditor.Document.Remove(currentOffset, 1);
                            lastInput = null;
                        }
                        break;
                    case "”":
                        if (lastInput == "“")
                        {
                            textEditor.Document.Remove(currentOffset, 1);
                            lastInput = null;
                        }
                        break;
                    case "’":
                        if (lastInput == "‘")
                        {
                            textEditor.Document.Remove(currentOffset, 1);
                            lastInput = null;
                        }
                        break;
                }
                switch (e.Text)
                {
                    case "(":
                        textEditor.Document.Insert(currentOffset, ")");
                        textEditor.TextArea.Caret.Offset = textEditor.TextArea.Caret.Offset - 1;
                        lastInput = "(";
                        break;
                    case "{":
                        textEditor.Document.Insert(currentOffset, "}");
                        textEditor.TextArea.Caret.Offset = textEditor.TextArea.Caret.Offset - 1;
                        lastInput = "{";
                        break;
                    case "<":
                        textEditor.Document.Insert(currentOffset, ">");
                        textEditor.TextArea.Caret.Offset = textEditor.TextArea.Caret.Offset - 1;
                        lastInput = "<";
                        break;
                    case "[":
                        textEditor.Document.Insert(currentOffset, "]");
                        textEditor.TextArea.Caret.Offset = textEditor.TextArea.Caret.Offset - 1;
                        lastInput = "[";
                        break;
                    case "\"":
                        textEditor.Document.Insert(currentOffset, "\"");
                        textEditor.TextArea.Caret.Offset = textEditor.TextArea.Caret.Offset - 1;
                        lastInput = "\"";
                        break;
                    case "'":
                        textEditor.Document.Insert(currentOffset, "'");
                        textEditor.TextArea.Caret.Offset = textEditor.TextArea.Caret.Offset - 1;
                        lastInput = "'";
                        break;
                    case "（":
                        textEditor.Document.Insert(currentOffset, "）");
                        textEditor.TextArea.Caret.Offset = textEditor.TextArea.Caret.Offset - 1;
                        lastInput = "（";
                        break;
                    case "【":
                        textEditor.Document.Insert(currentOffset, "】");
                        textEditor.TextArea.Caret.Offset = textEditor.TextArea.Caret.Offset - 1;
                        lastInput = "【";
                        break;
                    case "《":
                        textEditor.Document.Insert(currentOffset, "》");
                        textEditor.TextArea.Caret.Offset = textEditor.TextArea.Caret.Offset - 1;
                        lastInput = "《";
                        break;
                    case "“":
                        textEditor.Document.Insert(currentOffset, "”");
                        textEditor.TextArea.Caret.Offset = textEditor.TextArea.Caret.Offset - 1;
                        lastInput = "“";
                        break;
                    case "‘":
                        textEditor.Document.Insert(currentOffset, "’");
                        textEditor.TextArea.Caret.Offset = textEditor.TextArea.Caret.Offset - 1;
                        lastInput = "‘";
                        break;
                }
            }
        }
    }
}
