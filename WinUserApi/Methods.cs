using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace WinUserApi
{
    public static class Methods
    {
        public const string LibraryName = @"user32.dll";

        [DllImport(LibraryName, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport(LibraryName)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport(LibraryName)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport(LibraryName)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport(LibraryName)]
        //public static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport(LibraryName, CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport(LibraryName)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport(LibraryName)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport(LibraryName)]
        //public static extern bool GetCursorPos(out Point point);
        public static extern bool GetCursorPos(out POINT point);

        [DllImport(LibraryName)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport(LibraryName)]
        public static extern uint SendInput(uint nInputs, [In] Input[] pInputs, int cbSize);
    }
}
