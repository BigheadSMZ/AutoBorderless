
namespace AutoBorderless
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal class Form_CopyMenu : Form
    {
        private const int SRCCOPY = 0x00CC0020;
        private Timer refreshTimer;
        private IntPtr procID = IntPtr.Zero;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        static extern bool StretchBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest,
            IntPtr hdcSrc, int xSrc, int ySrc, int wSrc, int hSrc, int rop);

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        private void TimerTick()
        {
            // Redraw the screen on every tick and close when game is closed.
            this.Invalidate();
            if (!IsWindow(this.procID))
            {
                this.Close();
                this.refreshTimer.Stop();
                this.refreshTimer.Dispose();
            }
        }

        private void FormShown(object sender, EventArgs earg)
        {
            // Create a timer to refresh the window.
            this.refreshTimer = new Timer();
            this.refreshTimer.Interval = 16;
            this.refreshTimer.Tick += (s, e) => this.TimerTick();
            this.refreshTimer.Start();
        }

        private void FormMouseClick(object sender, MouseEventArgs e)
        {
            // When clicking the form, activate the game window to not lose focus.
            SetForegroundWindow(this.procID);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Draw the graphics if the game exists.
            if (this.procID == IntPtr.Zero)
                return;
            Graphics g = e.Graphics;
            IntPtr hdcDest = g.GetHdc();
            IntPtr hdcSrc = GetDC(this.procID);

            // Get the game window.
            RECT clientRect;
            GetClientRect(this.procID, out clientRect);
            int srcWidth = clientRect.Right - clientRect.Left;
            int srcHeight = clientRect.Bottom - clientRect.Top;
            Screen targetScreen = Screen.FromHandle(this.procID);
            Rectangle bounds = targetScreen.Bounds;

            // Draw the image and release device context.
            StretchBlt(hdcDest, 0, 0, bounds.Width, bounds.Height,
                       hdcSrc, 0, 0, srcWidth, srcHeight, SRCCOPY);
            ReleaseDC(this.procID, hdcSrc);
            g.ReleaseHdc(hdcDest);
        }

        public Form_CopyMenu(IntPtr GameProcess)
        {
            // Capture the handle.
            this.procID = GameProcess;

            // Set up the initial parameters of the form.
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.DoubleBuffered = true;
            this.Shown += new EventHandler(this.FormShown);
            this.MouseClick += new MouseEventHandler(this.FormMouseClick);
        }
    }
}
