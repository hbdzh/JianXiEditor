using ICSharpCode.AvalonEdit;
using JianXiEditor.Model;
using System.Windows;
namespace JianXiEditor.Service.EditorPlus
{
    class LineHandle
    {
        /// <summary>
        /// 双击复制当前行内容
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        /// <returns>true复制，false无需复制</returns>
        public bool CopyLineContent(TextEditor textEditor)
        {
            int currentOffset = textEditor.TextArea.Caret.Offset;//获取当前光标位置
            int currentNumber = textEditor.TextArea.Caret.Line;//获取当前行位置
            int currentLineOffset = textEditor.Document.GetLineByNumber(currentNumber).Offset;//获取当前行的起始位置
            int currentLineLength = textEditor.Document.GetLineByNumber(currentNumber).Length;//获取当前行的长度
            if (currentOffset == (currentLineLength + currentLineOffset))//如果一样，则代表光标在行的最后
            {
                Clipboard.SetText(textEditor.Document.GetText(currentLineOffset, currentLineLength));
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 剪切并删除行
        /// </summary>
        public void CutLine(TextEditor textEditor, MainModel main)
        {
            int lineBreak;
            if (main.FileLineBreak == "CRLF")
            {
                lineBreak = 2;
            }
            else
            {
                lineBreak = 1;
            }
            int currentLineNumber = textEditor.TextArea.Caret.Line;//获取当前行位置
            int currentLineOffset = textEditor.Document.GetLineByNumber(currentLineNumber).Offset;//获取当前行的起始位置
            int currentLineLength = textEditor.Document.GetLineByNumber(currentLineNumber).Length;//获取当前行的长度
            string currentLineContent = textEditor.Document.GetText(currentLineOffset, currentLineLength);//获取当前行内容
            if (currentLineContent.Length > 0)
            {
                Clipboard.SetText(currentLineContent);
                textEditor.Document.Remove(currentLineOffset, currentLineLength + lineBreak);
            }
        }
        /// <summary>
        /// 将当前行插入到下一行
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        public void CurrentLineInsertNextLine(TextEditor textEditor, MainModel main)
        {
            int currentLineNumber = textEditor.TextArea.Caret.Line;//获取当前行位置
            int currentLineOffset = textEditor.Document.GetLineByNumber(currentLineNumber).Offset;//获取当前行的起始位置
            int currentLineLength = textEditor.Document.GetLineByNumber(currentLineNumber).Length;//获取当前行的长度
            string currentLineContent = textEditor.Document.GetText(currentLineOffset, currentLineLength);//获取当前行内容
            string lineBreak = null;
            switch (main.FileLineBreak)
            {
                case "CRLF":
                    lineBreak = "\r\n";
                    break;
                case "LF":
                    lineBreak = "\n";
                    break;
                case "CR":
                    lineBreak = "\r";
                    break;
            }
            textEditor.Document.Insert(currentLineOffset + currentLineLength, lineBreak + currentLineContent);
        }
        /// <summary>
        /// 向上选择行
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        public void SelectUpLine(TextEditor textEditor)
        {
            int currentLineNumber = textEditor.TextArea.Caret.Line;//获取当前行位置
            int preLineNumber = currentLineNumber - 1;//上一行
            int currentLineOffset, currentLineLength;
            if (currentLineNumber > 1)//如果当前行不是第一行，就选择上一行
            {
                currentLineOffset = textEditor.Document.GetLineByNumber(preLineNumber).Offset;//获取上一行的起始位置
                currentLineLength = textEditor.Document.GetLineByNumber(preLineNumber).Length;//获取上一行的长度
                textEditor.Select(currentLineOffset, currentLineLength);
            }
            else if (currentLineNumber == 1)//如果当前行是第一行，就直接选择当前行
            {
                currentLineOffset = textEditor.Document.GetLineByNumber(currentLineNumber).Offset;//获取当前行的起始位置
                currentLineLength = textEditor.Document.GetLineByNumber(currentLineNumber).Length;//获取当前行的长度
                textEditor.Select(currentLineOffset, currentLineLength);
            }
        }
        /// <summary>
        /// 向下选择行
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        public void SelectDownLine(TextEditor textEditor)
        {
            int currentLineNumber = textEditor.TextArea.Caret.Line;//获取当前行位置
            int nextLineNumber = currentLineNumber + 1;//下一行
            int currentLineOffset, currentLineLength;
            if (currentLineNumber < textEditor.LineCount)//如果当前行不是最后一行，就选择下一行
            {
                currentLineOffset = textEditor.Document.GetLineByNumber(nextLineNumber).Offset;//获取下一行的起始位置
                currentLineLength = textEditor.Document.GetLineByNumber(nextLineNumber).Length;//获取下一行的长度
                textEditor.Select(currentLineOffset, currentLineLength);
            }
            else if (currentLineNumber == textEditor.LineCount)//如果当前行是最后一行，就选择当前行
            {
                currentLineOffset = textEditor.Document.GetLineByNumber(currentLineNumber).Offset;//获取当前行的起始位置
                currentLineLength = textEditor.Document.GetLineByNumber(currentLineNumber).Length;//获取当前行的长度
                textEditor.Select(currentLineOffset, currentLineLength);
            }
        }
        /// <summary>
        /// 向上移动行
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        /// <param name="main">视图模型</param>
        public void MoveUpLine(TextEditor textEditor, MainModel main)
        {
            //如果当前行不是第一行，就将当前行从位置上移除，然后插入到上一行的位置去
            //如果当前行是最后一行，就将上一行从位置上移除，插入到最后一行后面去
            int lineBreakLength;
            if (main.FileLineBreak == "CRLF")
            {
                lineBreakLength = 2;
            }
            else
            {
                lineBreakLength = 1;
            }
            int currentLineNumber = textEditor.TextArea.Caret.Line;//获取当前行位置
            int preLineNumber = currentLineNumber - 1;//上一行
            if (currentLineNumber > 1 && currentLineNumber != textEditor.LineCount)//如果当前行不是第一行并且不是最后一行
            {
                int currentLineOffset = textEditor.Document.GetLineByNumber(currentLineNumber).Offset - lineBreakLength;//获取当前行的起始位置
                int currentLineLength = textEditor.Document.GetLineByNumber(currentLineNumber).Length + lineBreakLength;//获取当前行的长度
                int preLineOffset = textEditor.Document.GetLineByNumber(preLineNumber).Offset;//获取上一行的起始位置
                string currentLineContent = textEditor.Document.GetText(currentLineOffset + lineBreakLength, currentLineLength);//获取当前行内容
                int caretOffset = textEditor.CaretOffset;//获取当前光标位置
                textEditor.Document.Remove(currentLineOffset, currentLineLength);
                textEditor.Document.Insert(preLineOffset, currentLineContent);
                int revOffset = caretOffset - currentLineOffset;//光标相对当前行的位置
                textEditor.CaretOffset = preLineOffset + revOffset - lineBreakLength;//上一行的光标起始位置加上光标相对位置，就是上一行光标的相对位置
                return;
            }
            else if (currentLineNumber == textEditor.LineCount)//如果当前行是最后一行
            {
                int preLineOffset = textEditor.Document.GetLineByNumber(preLineNumber).Offset - lineBreakLength;//获取上一行起始位置
                int preLineLength = textEditor.Document.GetLineByNumber(preLineNumber).Length;//获取上一行长度
                string preLineContent = textEditor.Document.GetText(preLineOffset, preLineLength + lineBreakLength);//获取上一行内容
                textEditor.Document.Remove(preLineOffset, preLineLength + lineBreakLength);
                textEditor.Document.Insert(textEditor.Text.Length, preLineContent);
                return;
            }
        }
        /// <summary>
        /// 向下移动行
        /// </summary>
        /// <param name="textEditor"></param>
        /// <param name="main">视图模型</param>
        public void MoveDownLine(TextEditor textEditor, MainModel main)
        {
            int currentLineNumber = textEditor.TextArea.Caret.Line;//获取当前行位置
            int nextLineNumber = currentLineNumber + 1;//下一行
            int lineBreakLength;
            if (main.FileLineBreak == "CRLF")//必须先判断换行符，因为不同换行符长度是不一样的
            {
                lineBreakLength = 2;
            }
            else
            {
                lineBreakLength = 1;
            }
            if (currentLineNumber > 1 && currentLineNumber != textEditor.LineCount && currentLineNumber != textEditor.LineCount - 1)//如果当前行不是第一行并且不是最后一行
            {
                int currentLineOffset = textEditor.Document.GetLineByNumber(currentLineNumber).Offset;//获取当前行的起始位置
                int nextLineOffset = textEditor.Document.GetLineByNumber(nextLineNumber).Offset;//获取下一行的起始位置
                int nextLineLength = textEditor.Document.GetLineByNumber(nextLineNumber).Length + lineBreakLength;//获取下一行的长度
                string nextLineContent = textEditor.Document.GetText(nextLineOffset, nextLineLength);//获取下一行内容
                textEditor.Document.Remove(nextLineOffset, nextLineLength);
                textEditor.Document.Insert(currentLineOffset, nextLineContent);
                return;
            }
            else if (currentLineNumber == 1)//如果当前行是第一行
            {
                int nextLineOffset = textEditor.Document.GetLineByNumber(nextLineNumber).Offset;//获取下一行起始位置
                int nextLineLength = textEditor.Document.GetLineByNumber(nextLineNumber).Length + lineBreakLength;//获取下一行长度
                string nextLineContent = textEditor.Document.GetText(nextLineOffset, nextLineLength);//获取下一行内容
                textEditor.Document.Remove(nextLineOffset, nextLineLength);//移除下一行的内容
                textEditor.Document.Insert(0, nextLineContent);//将下一行内容插到第一行
                return;
            }
            else if (currentLineNumber == textEditor.LineCount - 1)//如果当前行是最后一行的上一行
            {
                int nextLineOffset = textEditor.Document.GetLineByNumber(nextLineNumber).Offset - lineBreakLength;//获取下一行起始位置
                int nextLineLength = textEditor.Document.GetLineByNumber(nextLineNumber).Length + lineBreakLength;//获取下一行长度
                int currentLineOffset = textEditor.Document.GetLineByNumber(currentLineNumber).Offset;//获取当前行起始位置
                string nextLineContent = textEditor.Document.GetText(nextLineOffset, nextLineLength);//获取下一行内容
                textEditor.Document.Remove(nextLineOffset, nextLineLength);
                textEditor.Document.Insert(currentLineOffset - lineBreakLength, nextLineContent);
                return;
            }
        }
    }
}
