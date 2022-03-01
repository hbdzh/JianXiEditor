using ICSharpCode.AvalonEdit;

namespace JianXiEditor.Service.EditorPlus
{
    class TextHandle
    {
        /// <summary>
        /// 向左移动文字
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        public void MoveLeftWord(TextEditor textEditor)
        {
            if (textEditor.Document.Text.Length >= 2)//如果文件内容小于2，就没法移动，不再执行后续代码
            {
                int i = textEditor.CaretOffset;//获取当前光标位置
                if (i != 0)
                {
                    textEditor.Select(i - 1, 1);//选中光标位置上的内容
                    string text = textEditor.TextArea.Selection.GetText();//获取选中的内容
                    textEditor.Select(i - 1, 0);//将光标放在原先的位置上，但是不再选中任何内容。因为要获取的内容已经拿到了
                    string remove = textEditor.Document.Text.Remove(i - 1, 1);//移除

                    if (i == textEditor.Document.Text.Length)
                    {
                        textEditor.Document.Text = remove.Insert(i - 2, text);//插入
                        textEditor.SelectionStart = i - 2;//重新设置光标位置
                    }
                    else
                    {
                        textEditor.Document.Text = remove.Insert(i, text);//插入
                        textEditor.SelectionStart = i - 1;//重新设置光标位置
                    }
                    textEditor.SelectionLength = 0;
                }
            }
        }
        /// <summary>
        /// 向右移动文字
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        public void MoveRightWord(TextEditor textEditor)
        {
            if (textEditor.Document.Text.Length >= 2)//如果文件内容小于2，就没法移动，不再执行后续代码
            {
                int i = textEditor.CaretOffset;//获取当前光标位置
                if (i == 0)
                {
                    textEditor.Select(i, 1);//选中光标位置上的内容
                    string text = textEditor.TextArea.Selection.GetText();//获取选中的内容
                    textEditor.Select(i, 0);//将光标放在原先的位置上，但是不再选中任何内容。因为要获取的内容已经拿到了
                    string remove = textEditor.Document.Text.Remove(i, 1);//移除
                    textEditor.Document.Text = remove.Insert(i + 1, text);//插入
                }
                else if (i == textEditor.Document.Text.Length || i == textEditor.Document.Text.Length - 1)
                {
                    return;
                }
                else
                {
                    textEditor.Select(i + 1, 1);//选中光标位置上的内容
                    string text = textEditor.TextArea.Selection.GetText();//获取选中的内容
                    textEditor.Select(i + 1, 0);//将光标放在原先的位置上，但是不再选中任何内容。因为要获取的内容已经拿到了
                    string remove = textEditor.Document.Text.Remove(i + 1, 1);//移除
                    textEditor.Document.Text = remove.Insert(i, text);//插入
                }
                textEditor.SelectionStart = i + 1;//重新设置光标位置
                textEditor.SelectionLength = 0;
            }
        }
        /// <summary>
        /// 向左选择文字
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        public void SelectLeftWord(TextEditor textEditor)
        {
            int i = textEditor.CaretOffset;//获取当前光标位置
            if (i != 1 && i != 0)
            {
                if (textEditor.SelectionLength == 0)//如果是0，则代表没有选中内容，这时就只执行这行代码就可以
                {
                    textEditor.Select(i - 1, 1);
                }
                else//如果已经选中过其他内容，则需要再额外移动一次光标，否则会导致无法进行下一次选中
                {
                    textEditor.Select(i - 1, 1);
                    textEditor.SelectionStart = i - 2;
                }
            }
        }
        /// <summary>
        /// 向右选择文字
        /// </summary>
        /// <param name="textEditor">编辑器</param>
        public void SelectRightWord(TextEditor textEditor)
        {
            int i = textEditor.CaretOffset;
            if (i != textEditor.Document.Text.Length)
            {
                textEditor.Select(i, 1);
            }
        }
    }
}
