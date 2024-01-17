using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace WindowsFormsApp2
{
    public sealed class AppWindow
    {
        public IntPtr Handle { get; private set; }
        public string Name { get; private set; }
        public RECT Bounds => _bounds;

        public bool IsValid => IsWindow(Handle);

        public static AppWindow FindWindow(string windowName, int retry = 5, int interval = 500, bool throwTimeout = true)
        {
            if (windowName == null || windowName.Length == 0)
                throw new ArgumentException("Invalid 'windowName'");

            AppWindow ret = null;

            for (int i = 0; i < retry; i++)
            {
                ret = _FindWindow(windowName);
                if (ret != null)
                    break;
                Task.Delay(interval).Wait();
            }

            if (throwTimeout && ret == null)
                throw new TimeoutException($"Timeout find window. ({windowName})");

            return ret;
        }

        public void Move(int x, int y)
        {
            this._Move(x, y, Bounds.Width, Bounds.Height);
        }
        
        public void Move(Point location)
        {
            this._Move(location.X, location.Y, Bounds.Width, Bounds.Height);
        }

        public void Resize(int width, int height)
        {
            this._Move(Bounds.X, Bounds.Y, width, height);
        }

        public void Resize(Size size)
        {
            this._Move(Bounds.X, Bounds.Y, size.Width, size.Height);
        }

        public void UpdateBounds(RECT bounds)
        {
            this._Move(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }

        public void SetForeground()
        {
            if (SetForegroundWindow(Handle))
                return;
            throw new InvalidOperationException("Failed set foreground.");
        }

        public void Reload(int retry = 5, int interval = 500)
        {
            for (int i = 0; i < retry; i++)
            {
                if (_Reload())
                    return;
                Task.Delay(interval).Wait();
            }
            throw new TimeoutException("Failed reload window.");
        }

        #region Private
        private RECT _bounds;
        private readonly object _lock = new object();

        private AppWindow(IntPtr handle, string name, RECT bounds) 
        {
            Handle = handle;
            Name = name;
            _bounds = bounds;
        }

        private static AppWindow _FindWindow(string windowName)
        {
            AppWindow ret = null;

            var handle = FindWindow(null, windowName);
            if (handle != IntPtr.Zero)
            {
                var length = GetWindowTextLength(handle);
                var sb = new StringBuilder(length);
                GetWindowText(handle, sb, length + 1);
                GetWindowRect(handle, out RECT bounds);
                ret = new AppWindow(handle, sb.ToString(), bounds);
            }
            else
            {
                var sb = new StringBuilder();
                EnumWindows((hWnd, lp) =>
                {
                    var length = GetWindowTextLength(hWnd);
                    if (length == 0)
                        return true;

                    sb.Clear();
                    sb.Capacity = length;
                    GetWindowText(hWnd, sb, length + 1);
                    var name = sb.ToString();
                    if (name.StartsWith(windowName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        GetWindowRect(hWnd, out RECT bounds);
                        ret = new AppWindow(hWnd, sb.ToString(), bounds);
                        return false;
                    }

                    return true;
                }, IntPtr.Zero);
            }

            return ret;
        }

        private void _Move(int x, int y, int width, int height)
        {
            lock (_lock)
            {
                if (IsValid == false)
                    Reload();
                _bounds = new RECT(x, y, width, height);
                if (MoveWindow(Handle, x, y, width, height, true))
                    return;
            }
            throw new InvalidOperationException("Failed move window.");
        }

        private bool _Reload()
        {
            var window = FindWindow(Name, throwTimeout: false);
            if (window != null)
            {
                Handle = window.Handle;
                _bounds = window.Bounds;
            }
            return window != null;
        }
        #endregion

        #region DLL Import
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.I4)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.I4)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool bRepaint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr SetFocus(IntPtr hWnd);
        #endregion
    }
}
