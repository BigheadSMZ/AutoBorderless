using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AutoBorderless
{
    internal class Window
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern bool AdjustWindowRect(ref Rectangle lpRect, uint dwStyle, bool bMenu);

        [DllImport("user32.dll")]
        private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        // Define constants
        private const int GWL_STYLE      = -16;
        private const int GWL_EXSTYLE    = -20;
        private const uint WS_POPUP      = 0x80000000;
        private const uint WS_VISIBLE    = 0x10000000;
        private const uint WS_EX_TOPMOST = 0x00000008;

        public static bool IsVisible(Process process)
        {
            IntPtr hWnd = process.MainWindowHandle;
            if (hWnd != IntPtr.Zero && IsWindowVisible(hWnd))
                return true;
            return false;
        }

        public static bool SetBorderlessString(string searchString)
        {
            // Define the handle pointer.
            IntPtr hWnd = IntPtr.Zero;

            // Loop through all processes found and find by executable name or window title.
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.ProcessName == searchString)
                    hWnd = process.MainWindowHandle;
                else if (process.MainWindowTitle == searchString)
                    hWnd = process.MainWindowHandle;
                if (hWnd != IntPtr.Zero)
                    break;
            }
            // The process doesn't exist so we exit.
            if (hWnd == IntPtr.Zero)
                return false;

            // The process exists so try to set borderless window.
            SetBorderless(hWnd);
            return true;
        }

        public static void SetBorderless(IntPtr hWnd)
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
    }
}
