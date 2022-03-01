using HandyControl.Tools.Interop;
using System;
using System.Windows;
using System.Windows.Interop;

namespace JianXiEditor.Utility
{
    /// <summary>
    /// 移除系统菜单
    /// </summary>
    class SystemMenuHandle
    {
        Window win;
        public SystemMenuHandle(Window window)
        {
            win = window;
        }
        public const int WS_SYSMENU = 0x00080000;
        public const int GWL_STYLE = -16;
        public void OnSourceInitialized(object sender, EventArgs e)
        {
            WindowInteropHelper windowInteropHelper = new WindowInteropHelper(win);
            IntPtr hwnd = windowInteropHelper.Handle;
            int windowLong = InteropMethods.GetWindowLong(hwnd, GWL_STYLE);
            windowLong &= ~WS_SYSMENU;
            InteropMethods.SetWindowLongPtr(hwnd, GWL_STYLE, new IntPtr(windowLong));
        }
    }
}
