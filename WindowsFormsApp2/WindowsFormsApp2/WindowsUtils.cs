using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public int X { get => Left; set => Left = value; }
        public int Y { get => Top; set => Top = value; }
        public int Width { get => Right - Left; }
        public int Height { get => Bottom - Top; }
        public System.Drawing.Point Location { get => new System.Drawing.Point(Left, Top); }
        public System.Drawing.Size Size { get => new System.Drawing.Size(Width, Height); }

        public override bool Equals(object obj)
        {
            return obj is RECT rECT &&
                   Left == rECT.Left &&
                   Top == rECT.Top &&
                   Right == rECT.Right &&
                   Bottom == rECT.Bottom &&
                   Width == rECT.Width &&
                   Height == rECT.Height &&
                   EqualityComparer<Point>.Default.Equals(Location, rECT.Location) &&
                   EqualityComparer<Size>.Default.Equals(Size, rECT.Size);
        }

        public override int GetHashCode()
        {
            int hashCode = 1082896862;
            hashCode = hashCode * -1521134295 + Left.GetHashCode();
            hashCode = hashCode * -1521134295 + Top.GetHashCode();
            hashCode = hashCode * -1521134295 + Right.GetHashCode();
            hashCode = hashCode * -1521134295 + Bottom.GetHashCode();
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            hashCode = hashCode * -1521134295 + Location.GetHashCode();
            hashCode = hashCode * -1521134295 + Size.GetHashCode();
            return hashCode;
        }

        public System.Drawing.Rectangle ToRectangle()
        {
            return new System.Drawing.Rectangle(Location, Size);
        }

        public override string ToString()
        {
            return $"RECT[{Left}, {Top}, {Width}, {Height}]";
        }

        public static RECT operator +(RECT rt1, RECT rt2)
        {
            return new RECT(
                rt1.Left + rt2.Left,
                rt1.Top + rt2.Top,
                rt1.Right + rt2.Right,
                rt1.Bottom + rt2.Bottom);
        }
        public static RECT operator -(RECT rt1, RECT rt2)
        {
            return new RECT(
                rt1.Left - rt2.Left,
                rt1.Top - rt2.Top,
                rt1.Right - rt2.Right,
                rt1.Bottom - rt2.Bottom);
        }
        public static bool operator ==(RECT rt1, RECT rt2)
        {
            return (
                rt1.Left == rt2.Left
                && rt1.Top == rt2.Top
                && rt1.Right == rt2.Right
                && rt1.Bottom == rt2.Bottom);
        }
        public static bool operator !=(RECT rt1, RECT rt2)
        {
            return (
                rt1.Left != rt2.Left
                || rt1.Top != rt2.Top
                || rt1.Right != rt2.Right
                || rt1.Bottom != rt2.Bottom);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is POINT pOINT &&
                   X == pOINT.X &&
                   Y == pOINT.Y;
        }
        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"POINT[{X}, {Y}]";
        }

        public static POINT operator +(POINT pt1, POINT pt2)
        {
            return new POINT(pt1.X + pt2.X, pt1.Y + pt2.Y);
        }
        public static POINT operator -(POINT pt1, POINT pt2)
        {
            return new POINT(pt1.X - pt2.X, pt1.Y - pt2.Y);
        }
        public static bool operator ==(POINT pt1, POINT pt2)
        {
            return (pt1.X == pt2.X && pt1.Y == pt2.Y);
        }
        public static bool operator !=(POINT pt1, POINT pt2)
        {
            return (pt1.X != pt2.X || pt1.Y != pt2.Y);
        }
    }

    public struct Test
    {
        public int x1;
        public int x2;
    }

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

        public int Move(Point location)
        {
            return _MoveWindow(location.X, location.Y, Bounds.Width, Bounds.Height);
        }

        public int Move(int x, int y)
        {
            return _MoveWindow(x, y, Bounds.Width, Bounds.Height);
        }

        public int Resize(Size size)
        {
            return _MoveWindow(Bounds.X, Bounds.Y, size.Width, size.Height);
        }

        public int Resize(int width, int height)
        {
            return _MoveWindow(Bounds.X, Bounds.Y, width, height);
        }

        private int _MoveWindow(int x, int y, int width, int height)
        {
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

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool bRepaint);
    }

    public partial class MainForm : Form
    {
        private WindowInfo FindWindow(string windowName)
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
                    if (builder.ToString().StartsWith(windowName))
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
    }

    public partial class MainForm : Form
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.I4)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

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
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
