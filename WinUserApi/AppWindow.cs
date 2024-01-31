using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using WinUserApi.Internal;
using Emgu.CV;
using Emgu.Util;

namespace WinUserApi
{
    public sealed class AppWindow
    {
        public IntPtr Handle { get; private set; }
        public string Name { get; private set; }
        public Rectangle Bounds { get; private set; }

        public AppWindow(Form form)
        {
            Handle = form.Handle;
            Name = form.Name;
            Bounds = form.Bounds;
        }

        public static AppWindow FindWindow(string windowName, int retry = 5, int interval = 500)
        {
            return FindWIndowAsync(windowName, retry, interval).Result;
        }

        public static async Task<AppWindow> FindWIndowAsync(string windowName, int retry = 5, int interval = 500)
        {
            AppWindow ret = null;

            for (int i = 0; i < retry; i++)
            {
                var handle = _FindWindowHandle(windowName);
                if (handle != IntPtr.Zero)
                {
                    var length = Methods.GetWindowTextLength(handle);
                    var sb = new StringBuilder(length);
                    Methods.GetWindowText(handle, sb, length + 1);
                    Methods.GetWindowRect(handle, out var bounds);
                    ret = new AppWindow(handle, sb.ToString(), bounds);
                    break;
                }
                await Task.Delay(interval);
            }

            return ret;
        }

        public static AppWindow GetForegroundWindow()
        {
            var handle = Methods.GetForegroundWindow();
            if (handle == IntPtr.Zero) return null;

            var length = Methods.GetWindowTextLength(handle);
            var sb = new StringBuilder(length);
            Methods.GetWindowText(handle, sb, length + 1);
            Methods.GetWindowRect(handle, out var bounds);
            return new AppWindow(handle, sb.ToString(), bounds);
        }

        public void SetForeground()
        {
            Methods.SetForegroundWindow(Handle);
        }

        public void Move(Point point)
        {
            this.Move(point.X, point.Y);
        }

        public void Move(int x, int y)
        {
            _bounds.X = x;
            _bounds.Y = y;
            Methods.MoveWindow(Handle, x, y, _bounds.Width, _bounds.Height, true);
        }

        public void Resize(Size size)
        {
            this.Resize(size.Width, size.Height);
        }

        public void Resize(int width, int height)
        {
            _bounds.Width = width;
            _bounds.Height = height;
            Methods.MoveWindow(Handle, _bounds.X, _bounds.Y, width, height, true);
        }

        public void UpdateBounds(Rectangle bounds)
        {
            this.UpdateBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }

        public void UpdateBounds(Point location, Size size)
        {
            this.UpdateBounds(location.X, location.Y, size.Width, size.Height);
        }

        public void UpdateBounds(int x, int y, int width, int height)
        {
            _bounds = new Rectangle(x, y, width, height);
            Methods.MoveWindow(Handle, _bounds.X, _bounds.Y, _bounds.Width, _bounds.Height, true);
        }

        #region Private
        private Rectangle _bounds;
        private bool disposedValue;

        private AppWindow(IntPtr handle, string name, Rectangle bounds)
        {
            Handle = handle;
            Name = name;
            Bounds = bounds;
        }

        private static IntPtr _FindWindowHandle(string windowName)
        {
            var handle = Methods.FindWindow(null, windowName);

            if (handle == IntPtr.Zero)
            {
                Methods.EnumWindows((hWnd, lParam) =>
                {
                    var length = Methods.GetWindowTextLength(hWnd);
                    if (length == 0) return true;

                    var sb = new StringBuilder(length);
                    Methods.GetWindowText(hWnd, sb, length + 1);
                    if (sb.ToString().StartsWith(windowName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        handle = hWnd;
                        return false;
                    }

                    return true;
                }, IntPtr.Zero);
            }

            return handle;
        }
        #endregion
    }
}
