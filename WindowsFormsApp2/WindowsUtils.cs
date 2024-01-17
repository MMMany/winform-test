using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;

namespace WindowsFormsApp2
{
    public class WindowInfo
    {
        public IntPtr Handle { get; private set; }
        private RECT _bounds;
        public RECT Bounds { get => _bounds; }
        public string Name { get; private set; }

        public WindowInfo(IntPtr handle, RECT bounds, string name)
        {
            Handle = handle;
            _bounds = bounds;
            Name = name;
        }

        public bool IsValid()
        {
            return IsWindow(Handle);
        }

        public void Move(int x, int y)
        {
            var errCode = _MoveWindow(x, y, Bounds.Width, Bounds.Height);
            if (errCode != 1)
            {
                // err
            }
        }

        public void Resize(int width, int height)
        {
            var errCode = _MoveWindow(Bounds.X, Bounds.Y, width, height);
            if (errCode != 1)
            {
                // err
            }
        }

        public void SetFocus()
        {
            if (Handle != IntPtr.Zero)
                SetFocus(Handle);
        }

        public void SetForeground()
        {
            if (Handle != IntPtr.Zero)
                SetForegroundWindow(Handle);
        }

        private int _MoveWindow(int x, int y, int width, int height)
        {
            if (Handle == IntPtr.Zero)
                return 0;

            var success = MoveWindow(Handle, x, y, width, height, true);
            var errCode = 1;
            if (success)
            {
                _bounds.X = x;
                _bounds.Y = y;
                _bounds.Right = x + width;
                _bounds.Bottom = y + height;
            }
            else
            {
                errCode = Marshal.GetLastWin32Error();
            }
            return errCode;
        }

        #region DLL Import
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

    public static class WindowsUtil
    {
        public static WindowInfo FindWindow(string windowName)
        {
            if (windowName == null || windowName.Trim().Length == 0)
                return null;

            WindowInfo ret = null;

            var handle = FindWindow(null, windowName);
            if (handle != IntPtr.Zero)
            {
                var length = GetWindowTextLength(handle);
                var builder = new StringBuilder(length);
                GetWindowText(handle, builder, length + 1);
                GetWindowRect(handle, out RECT bounds);
                ret = new WindowInfo(handle, bounds, builder.ToString());
            }
            else
            {
                EnumWindows(new EnumWindowsProc((hWnd, param) =>
                {
                    var length = GetWindowTextLength(hWnd);
                    if (length == 0)
                        return true;

                    var builder = new StringBuilder(length);
                    GetWindowText(hWnd, builder, length + 1);
                    if (builder.ToString().StartsWith(windowName, comparisonType: StringComparison.CurrentCultureIgnoreCase))
                    {
                        GetWindowRect(hWnd, out RECT bounds);
                        ret = new WindowInfo(hWnd, bounds, builder.ToString());
                        return false;
                    }

                    return true;
                }), IntPtr.Zero);
            }

            return ret;
        }

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
        #endregion
    }
}
