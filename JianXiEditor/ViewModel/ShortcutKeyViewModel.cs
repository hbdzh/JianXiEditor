using System.Collections.Generic;
using HandyControl.Tools;
using HandyControl.Tools.Command;
using JianXiEditor.Model;

namespace JianXiEditor.ViewModel
{
    class ShortcutKeyViewModel : BindablePropertyBase
    {
        private ShortcutKeyModel shortcutKeys;
        public ShortcutKeyModel ShortcutKeys
        {
            get
            {
                if (shortcutKeys == null)//如果不是空的，就说明已经实例化了
                {
                    shortcutKeys = new ShortcutKeyModel();
                }
                return shortcutKeys;
            }
            set => Set(ref shortcutKeys, value);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public RelayCommand ShortcutKeyLoadedCommand => new RelayCommand(obj =>
        {
            ShortcutKeys.ShortcutKeyList = new List<ShortcutKeyModel>
            {
                new ShortcutKeyModel { ShortcutKey = "Ctrl+N", FunExplain = "新建文件" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+O", FunExplain = "打开文件" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+S", FunExplain = "保存文件" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+Alt+S", FunExplain = "文件另存为" },
                new ShortcutKeyModel { ShortcutKey = "", FunExplain = "" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+C", FunExplain = "复制选中内容" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+X", FunExplain = "剪切选中内容" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+V", FunExplain = "粘贴到光标所在处" },
                new ShortcutKeyModel { ShortcutKey = "Delete", FunExplain = "删除选中内容" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+A", FunExplain = "选中所有内容" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+Z", FunExplain = "撤消上一个操作" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+Y", FunExplain = "恢复上一个操作" },
                new ShortcutKeyModel { ShortcutKey = "", FunExplain = "" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+W", FunExplain = "网络搜索选中内容" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+F", FunExplain = "使用查找功能" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+P", FunExplain = "使用打印功能" },
                new ShortcutKeyModel { ShortcutKey = "", FunExplain = "" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+K", FunExplain = "注释/取消注释" },
                new ShortcutKeyModel { ShortcutKey = "Alt+Left", FunExplain = "向左选择/移动文字" },
                new ShortcutKeyModel { ShortcutKey = "Alt+Right", FunExplain = "向右选择/移动文字" },
                new ShortcutKeyModel { ShortcutKey = "Alt+Up", FunExplain = "向上选择/移动行" },
                new ShortcutKeyModel { ShortcutKey = "Alt+Down", FunExplain = "向下选择/移动行" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+D", FunExplain = "删除当前行" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+L", FunExplain = "当前行插入下一行" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+Shift+C", FunExplain = "复制整个文档" },
                new ShortcutKeyModel { ShortcutKey = "Ctrl+Shift+X", FunExplain = "剪切并删除行" },
                new ShortcutKeyModel { ShortcutKey = "Alt+F", FunExplain = "跳转到相同内容" },
                new ShortcutKeyModel { ShortcutKey = "Alt+MouseLeft", FunExplain = "块选择" }
            };
        });
    }
}