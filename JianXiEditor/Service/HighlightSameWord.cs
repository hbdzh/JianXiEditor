using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace JianXiEditor.Service
{
    /// <summary>
    /// 高亮选中文字
    /// </summary>
    public class HighlightSameWord : DocumentColorizingTransformer
    {
        static TextEditor textEditor;
        protected override void ColorizeLine(DocumentLine line)
        {
            string text = CurrentContext.Document.GetText(line);
            string selectedText = textEditor.TextArea.Selection.GetText();
            string pattern = "\\b" + selectedText + "\\b";
            bool isMatch = false;
            try
            {
                isMatch = Regex.IsMatch(text, pattern);
            }
            catch
            {
                isMatch = false;
            }
            if (isMatch == true)
            {
                int lineStartOffset = line.Offset;
                int start = 0;
                int index;
                while ((index = text.IndexOf(selectedText, start, StringComparison.Ordinal)) >= 0)
                {
                    ChangeLinePart(
                      lineStartOffset + index, //startOffset
                      lineStartOffset + index + selectedText.Length, //endOffset
                       (VisualLineElement element) =>
                       {
                           // This lambda gets called once for every VisualLineElement
                           // between the specified offsets.
                           Typeface tf = element.TextRunProperties.Typeface;
                           // Replace the typeface with a modified version of
                           // the same typeface
                           element.TextRunProperties.SetTypeface(new Typeface(
                               tf.FontFamily,
                               FontStyles.Oblique,
                               FontWeights.Bold,
                               tf.Stretch
                           ));
                       });
                    start = index + 1; //search for next occurrence
                }
            }
        }
        /// <summary>
        /// 监听事件，并高亮显示
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        public static void HiSelect(TextEditor editor)
        {
            textEditor = editor;
            textEditor.TextArea.SelectionChanged += (sender, args) =>
            {
                if (Config.Config_App.Config_HighlightSameWord == true)
                {
                    if (string.IsNullOrWhiteSpace(textEditor.SelectedText))
                    {
                        foreach (var markSameWord in textEditor.TextArea.TextView.LineTransformers.OfType<HighlightSameWord>().ToList())
                        {
                            textEditor.TextArea.TextView.LineTransformers.Remove(markSameWord);
                        }
                    }
                    else
                    {
                        textEditor.TextArea.TextView.LineTransformers.Add(new HighlightSameWord());
                    }
                }
            };
        }
    }
}
