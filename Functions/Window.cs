using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoBorderless
{
    internal class Window
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern bool AdjustWindowRect(ref Rectangle lpRect, long dwStyle, bool bMenu);

        [DllImport("user32.dll")]
        private static extern long GetWindowLong(IntPtr hWnd, long nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        // Define constants
        private const int GWL_STYLE      = -16;
        private const int GWL_EXSTYLE    = -20;

        private const int WS_BORDER = 0x00800000;
        private const int WS_DLGFRAME = 0x00400000;
        private const int WS_CAPTION = WS_BORDER | WS_DLGFRAME;
        private const int WS_SYSMENU = 0x00080000;
        private const int WS_THICKFRAME = 0x00040000;
        private const int WS_MINIMIZEBOX = 0x00020000;
        private const int WS_MAXIMIZEBOX = 0x00010000;

        private const long WS_POPUP      = 0x80000000;
        private const long WS_VISIBLE    = 0x10000000;
        private const long WS_EX_TOPMOST = 0x00000008;

        private const int SWP_FRAMECHANGED = 0x0020;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOOWNERZORDER = 0x0200;
        private const int SWP_NOSENDCHANGING = 0x0400;

        public static bool IsVisible(Process process)
        {
            IntPtr hWnd = process.MainWindowHandle;
            if (hWnd != IntPtr.Zero && IsWindowVisible(hWnd))
                return true;
            return false;
        }
        /*
        public static void SetBorderlessA(IntPtr hWnd)
        {
            // Get screen size (based on primary monitor)
            int screenWidth  = SystemInformation.PrimaryMonitorSize.Width;
            int screenHeight = SystemInformation.PrimaryMonitorSize.Height;

            // Rectangle
            Rectangle winRect = new Rectangle(0, 0, screenWidth, screenHeight);

            // Move and resize window to cover the full screen
            SetWindowLong(hWnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
            AdjustWindowRect(ref winRect, GetWindowLong(hWnd, GWL_STYLE), false);
            SetWindowLong(hWnd, GWL_EXSTYLE, (GetWindowLong(hWnd, GWL_EXSTYLE) | WS_EX_TOPMOST));
            MoveWindow(hWnd, winRect.Left, winRect.Top, winRect.Right - winRect.Left, winRect.Bottom - winRect.Top, true);
        }
        */
        public static void SetBorderless(IntPtr hWnd)
        {
            long style = GetWindowLong(hWnd, GWL_STYLE);
            style &= ~(WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);
            SetWindowLong(hWnd, GWL_STYLE, style);

            // Get the screen the window is on
            Screen screen = Screen.FromHandle(hWnd);
            var bounds = screen.Bounds;

            SetWindowPos(hWnd, IntPtr.Zero, bounds.X, bounds.Y, bounds.Width, bounds.Height,
                SWP_FRAMECHANGED | SWP_SHOWWINDOW | SWP_NOZORDER | SWP_NOOWNERZORDER | SWP_NOSENDCHANGING);
        }

    }
}
