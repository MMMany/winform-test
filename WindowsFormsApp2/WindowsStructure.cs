using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
        public RECT(Point location, Size size)
        {
            (Left, Top) = (location.X, location.Y);
            (Right, Bottom) = (Left + size.Width, Top + size.Height);
        }

        public static RECT Zero => new RECT(0, 0, 0, 0);

        public int X { get => Left; set => Left = value; }
        public int Y { get => Top; set => Top = value; }
        public int Width { get => Right - Left; set => Right = Left + value; }
        public int Height { get => Bottom - Top; set => Bottom = Top + value; }
        public Point Location { get => new Point(Left, Top); set => (X, Y) = (value.X, value.Y); }
        public Size Size { get => new Size(Width, Height); set => (Width, Height) = (value.Width, value.Height); }
        public Point Center { get => new Point((int)(X + Width / 2), (int)(Y + Height / 2)); }

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

        public Rectangle ToRectangle()
        {
            return (Rectangle)this;
        }

        public override string ToString()
        {
            return $"RECT[{Left}, {Top}, {Width}, {Height}]";
        }

        public static implicit operator Rectangle(RECT bds) => new Rectangle(bds.Location, bds.Size);
        public static explicit operator RECT(Rectangle bds) => new RECT(bds.Location, bds.Size);
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

        public Point ToPoint()
        {
            return (Point)this;
        }

        public static implicit operator Point(POINT pt) => new Point(pt.X, pt.Y);
        public static explicit operator POINT(Point pt) => new POINT(pt.X, pt.Y);
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
}
