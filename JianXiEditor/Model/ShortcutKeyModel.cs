using HandyControl.Tools;
using System.Collections.Generic;

namespace JianXiEditor.Model
{
    class ShortcutKeyModel : BindablePropertyBase
    {
        private List<ShortcutKeyModel> shortcutKeyList;
        /// <summary>
        /// 快捷键列表
        /// </summary>
        public List<ShortcutKeyModel> ShortcutKeyList
        {
            get => shortcutKeyList;
            set => Set(ref shortcutKeyList, value);
        }
        private string shortcutKey;
        /// <summary>
        /// 快捷键
        /// </summary>
        public string ShortcutKey
        {
            get => shortcutKey;
            set => Set(ref shortcutKey, value);
        }
        private string funExplain;
        /// <summary>
        /// 说明
        /// </summary>
        public string FunExplain
        {
            get => funExplain;
            set => Set(ref funExplain, value);
        }
    }
}
