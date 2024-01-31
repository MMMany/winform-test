using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinUserApi
{
    public enum MouseInputType
    {
        Left,
        Right,
        Middle,
    }

    public static class WindowsInput
    {
        public static void MouseClick(Point point, MouseInputType type)
        {
            MouseClick(point.X, point.Y, type);
        }

        public static void MouseClick(int x, int y, MouseInputType type)
        {
            var flags = MouseInputFlags.LEFTDOWN;

            if (type == MouseInputType.Right)
                flags = (MouseInputFlags)((uint)flags << 2);
            else if (type == MouseInputType.Middle)
                flags = (MouseInputFlags)((uint)flags << 4);
            else
                throw new InvalidOperationException("Not supported mouse event");

            Input.InitMouseInput(out var down, x, y, flags);
            flags = (MouseInputFlags)((uint)flags << 1);
            Input.InitMouseInput(out var up, x, y, flags);

            Methods.SendInput(2, new[] { down, up }, Marshal.SizeOf(typeof(Input)));
        }

        public static void MouseDown(Point point, MouseInputType type)
        {
            MouseDown(point.X, point.Y, type);
        }

        public static void MouseDown(int x, int y, MouseInputType type)
        {
            var flags = MouseInputFlags.LEFTDOWN;

            if (type == MouseInputType.Right)
                flags = (MouseInputFlags)((uint)flags << 2);
            else if (type == MouseInputType.Middle)
                flags = (MouseInputFlags)((uint)flags << 4);
            else
                throw new InvalidOperationException("Not supported mouse event");

            Input.InitMouseInput(out var input, x, y, flags);

            Methods.SendInput(1, new[] { input }, Marshal.SizeOf(typeof(Input)));
        }

        public static void MouseUp(Point point, MouseInputType type)
        {
            MouseUp(point.X, point.Y, type);
        }

        public static void MouseUp(int x, int y, MouseInputType type)
        {
            var flags = MouseInputFlags.LEFTUP;

            if (type == MouseInputType.Right)
                flags = (MouseInputFlags)((uint)flags << 2);
            else if (type == MouseInputType.Middle)
                flags = (MouseInputFlags)((uint)flags << 4);
            else
                throw new InvalidOperationException("Not supported mouse event");

            Input.InitMouseInput(out var input, x, y, flags);

            Methods.SendInput(1, new[] { input }, Marshal.SizeOf(typeof(Input)));
        }

        public static void KeyboardPress(VirtualKey key)
        {
            Input.InitKeyboardInput(out var down, key, false);
            Input.InitKeyboardInput(out var up, key, true);

            Methods.SendInput(2, new[] { down, up }, Marshal.SizeOf(typeof(Input)));
        }

        public static void KeyboardDown(VirtualKey key)
        {
            Input.InitKeyboardInput(out var input, key, false);

            Methods.SendInput(1, new[] { input }, Marshal.SizeOf(typeof(Input)));
        }

        public static void KeyboardUp(VirtualKey key)
        {
            Input.InitKeyboardInput(out var input, key, true);

            Methods.SendInput(1, new[] { input }, Marshal.SizeOf(typeof(Input)));
        }
    }
}
