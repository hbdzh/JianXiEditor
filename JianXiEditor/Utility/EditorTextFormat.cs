using ICSharpCode.AvalonEdit;
using System.Windows;

namespace JianXiEditor.Utility
{
    class EditorTextFormat
    {
        /// <summary>
        /// 取消带格式的文本复制
        /// </summary>
        public static void onTextViewSettingDataHandler(object sender, DataObjectSettingDataEventArgs e)
        {
            TextEditor textView = sender as TextEditor;
            if (textView != null && e.Format == DataFormats.Html)
            {
                e.CancelCommand();
            }
        }
    }
}
