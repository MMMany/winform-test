using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.Structure;

namespace WindowsFormsApp2
{
    public sealed class AppWindow
    {
        public IntPtr Handle { get; private set; }
        public string Name { get; private set; }
        public RECT Bounds { get; private set; }

        public static AppWindow FindWindow(string windowName, int retry = 5, int interval = 500, bool throwTimeout = true)
        {
            if (windowName == null || windowName.Length == 0)
                throw new ArgumentException($"Invalid 'windowName'");

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

        #region Private
        private AppWindow(IntPtr handle, string name, RECT bounds) 
        {
            Handle = handle;
            Name = name;
            Bounds = bounds;
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
