using HandyControl.Tools;
namespace JianXiEditor.Model
{
    class ToLineModel : BindablePropertyBase
    {
        private int toLineNumber;
        /// <summary>
        /// 要跳转的行号
        /// </summary>
        public int ToLineNumber
        {
            get => toLineNumber;
            set => Set(ref toLineNumber, value);
        }
    }
}
