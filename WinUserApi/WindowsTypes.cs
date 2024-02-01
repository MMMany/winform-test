using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace WinUserApi
{
    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            (Left, Top, Right, Bottom) = (left, top, right, bottom);
        }

        public RECT(Point location, Size size)
        {
            (Left, Top) = (location.X, location.Y);
            (Right, Bottom) = (location.X + size.Width, location.Y + size.Height);
        }

        public static RECT Empty => new RECT(0, 0, 0, 0);

        public int X { get => Left; set => Left = value; }
        public int Y { get => Top; set => Top = value; }
        public int Width { get => Right - Left; set => Right = Left + value; }
        public int Height { get => Bottom - Top; set => Bottom = Top + value; }

        public Point Location
        {
            get => new Point(X, Y);
            set
            {
                var (width, height) = (Width, Height);
                (X, Y) = (value.X, value.Y);
                (Width, Height) = (width, height);
            }
        }
        public Size Size
        {
            get => new Size(Width, Height);
            set => (Width, Height) = (value.Width, value.Height);
        }

        public Point Center => new Point(X + (int)(Width / 2), Y + (int)(Height / 2));

        public override string ToString() => $"RECT[{Left}, {Top}, {Right}, {Bottom}]";

        public override bool Equals(object obj)
        {
            return obj is RECT rECT &&
                   Left == rECT.Left &&
                   Top == rECT.Top &&
                   Right == rECT.Right &&
                   Bottom == rECT.Bottom;
        }

        public override int GetHashCode()
        {
            int hashCode = -1819631549;
            hashCode = hashCode * -1521134295 + Left.GetHashCode();
            hashCode = hashCode * -1521134295 + Top.GetHashCode();
            hashCode = hashCode * -1521134295 + Right.GetHashCode();
            hashCode = hashCode * -1521134295 + Bottom.GetHashCode();
            return hashCode;
        }

        public static implicit operator Rectangle(RECT rect) => new Rectangle(rect.Location, rect.Size);
        public static implicit operator RECT(Rectangle rect) => new RECT(rect.Location, rect.Size);

        public static bool operator ==(RECT left, RECT right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(RECT left, RECT right)
        {
            return !(left == right);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            (X, Y) = (x, y);
        }

        public override string ToString() => $"POINT[{X}, {Y}]";

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

        public static bool operator ==(POINT left, POINT right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(POINT left, POINT right)
        {
            return !(left == right);
        }

        public static implicit operator Point(POINT point) => new Point(point.X, point.Y);
        public static implicit operator POINT(Point point) => new POINT(point.X, point.Y);
    }

    [Flags]
    public enum MouseInputFlags
    {
        MOVE = 0x0001,
        LEFTDOWN = 0x0002,
        LEFTUP = 0x0004,
        RIGHTDOWN = 0x0008,
        RIGHTUP = 0x0010,
        MIDDLEDOWN = 0x0020,
        MIDDLEUP = 0x0040,
        XDOWN = 0x0080,
        XUP = 0x0100,
        WHEEL = 0x0800,
        HWHEEL = 0x1000,
        MOVE_NOCOALESCE = 0x2000,
        VIRTUALDESK = 0x4000,
        ABSOLUTE = 0x8000,
    }

    [Flags]
    public enum KeyboardInputFlags
    {
        EXTENDED = 0x0001,
        KEYUP = 0x0002,
        UNICODE = 0x0004,
        SCANCODE = 0x0008,
    }

    public enum InputType
    {
        MOUSE = 0,
        KEYBOARD = 1,
        HARDWARE = 2,
    }

    public enum VirtualKey
    {
        LBUTTON = 0x01,
        RBUTTON = 0x02,
        CANCEL = 0x03,
        MBUTTON = 0x04 /* NOT contiguous with L & RBUTTON */,
        XBUTTON1 = 0x05 /* NOT contiguous with L & RBUTTON */,
        XBUTTON2 = 0x06 /* NOT contiguous with L & RBUTTON */,
        BACK = 0x08,
        TAB = 0x09,
        CLEAR = 0x0C,
        RETURN = 0x0D,
        SHIFT = 0x10,
        CONTROL = 0x11,
        MENU = 0x12,
        PAUSE = 0x13,
        CAPITAL = 0x14,
        KANA = 0x15,
        HANGEUL = 0x15 /* old name - should be here for compatibility */,
        HANGUL = 0x15,
        JUNJA = 0x17,
        FINAL = 0x18,
        HANJA = 0x19,
        KANJI = 0x19,
        ESCAPE = 0x1B,
        CONVERT = 0x1C,
        NONCONVERT = 0x1D,
        ACCEPT = 0x1E,
        MODECHANGE = 0x1F,
        SPACE = 0x20,
        PRIOR = 0x21,
        NEXT = 0x22,
        END = 0x23,
        HOME = 0x24,
        LEFT = 0x25,
        UP = 0x26,
        RIGHT = 0x27,
        DOWN = 0x28,
        SELECT = 0x29,
        PRINT = 0x2A,
        EXECUTE = 0x2B,
        SNAPSHOT = 0x2C,
        INSERT = 0x2D,
        DELETE = 0x2E,
        HELP = 0x2F,
        LWIN = 0x5B,
        RWIN = 0x5C,
        APPS = 0x5D,
        SLEEP = 0x5F,
        NUMPAD0 = 0x60,
        NUMPAD1 = 0x61,
        NUMPAD2 = 0x62,
        NUMPAD3 = 0x63,
        NUMPAD4 = 0x64,
        NUMPAD5 = 0x65,
        NUMPAD6 = 0x66,
        NUMPAD7 = 0x67,
        NUMPAD8 = 0x68,
        NUMPAD9 = 0x69,
        MULTIPLY = 0x6A,
        ADD = 0x6B,
        SEPARATOR = 0x6C,
        SUBTRACT = 0x6D,
        DECIMAL = 0x6E,
        DIVIDE = 0x6F,
        F1 = 0x70,
        F2 = 0x71,
        F3 = 0x72,
        F4 = 0x73,
        F5 = 0x74,
        F6 = 0x75,
        F7 = 0x76,
        F8 = 0x77,
        F9 = 0x78,
        F10 = 0x79,
        F11 = 0x7A,
        F12 = 0x7B,
        F13 = 0x7C,
        F14 = 0x7D,
        F15 = 0x7E,
        F16 = 0x7F,
        F17 = 0x80,
        F18 = 0x81,
        F19 = 0x82,
        F20 = 0x83,
        F21 = 0x84,
        F22 = 0x85,
        F23 = 0x86,
        F24 = 0x87,
        NUMLOCK = 0x90,
        SCROLL = 0x91,
        OEM_NEC_EQUAL = 0x92, // '=' key on numpad,
        OEM_FJ_JISHO = 0x92, // 'Dictionary' key,
        OEM_FJ_MASSHOU = 0x93, // 'Unregister word' key,
        OEM_FJ_TOUROKU = 0x94, // 'Register word' key,
        OEM_FJ_LOYA = 0x95, // 'Left OYAYUBI' key,
        OEM_FJ_ROYA = 0x96, // 'Right OYAYUBI' key,
        LSHIFT = 0xA0,
        RSHIFT = 0xA1,
        LCONTROL = 0xA2,
        RCONTROL = 0xA3,
        LMENU = 0xA4,
        RMENU = 0xA5,
        BROWSER_BACK = 0xA6,
        BROWSER_FORWARD = 0xA7,
        BROWSER_REFRESH = 0xA8,
        BROWSER_STOP = 0xA9,
        BROWSER_SEARCH = 0xAA,
        BROWSER_FAVORITES = 0xAB,
        BROWSER_HOME = 0xAC,
        VOLUME_MUTE = 0xAD,
        VOLUME_DOWN = 0xAE,
        VOLUME_UP = 0xAF,
        MEDIA_NEXT_TRACK = 0xB0,
        MEDIA_PREV_TRACK = 0xB1,
        MEDIA_STOP = 0xB2,
        MEDIA_PLAY_PAUSE = 0xB3,
        LAUNCH_MAIL = 0xB4,
        LAUNCH_MEDIA_SELECT = 0xB5,
        LAUNCH_APP1 = 0xB6,
        LAUNCH_APP2 = 0xB7,
        OEM_1 = 0xBA, // ';:' for US,
        OEM_PLUS = 0xBB, // '+' any country,
        OEM_COMMA = 0xBC, // ',' any country,
        OEM_MINUS = 0xBD, // '-' any country,
        OEM_PERIOD = 0xBE, // '.' any country,
        OEM_2 = 0xBF, // '/?' for US,
        OEM_3 = 0xC0, // '`~' for US,
        OEM_4 = 0xDB, //  '[{' for US,
        OEM_5 = 0xDC, //  '\|' for US,
        OEM_6 = 0xDD, //  ']}' for US,
        OEM_7 = 0xDE, //  ''"' for US,
        OEM_8 = 0xDF,
        OEM_AX = 0xE1, //  'AX' key on Japanese AX kbd,
        OEM_102 = 0xE2, //  "<>" or "\|" on RT 102-key kbd.,
        ICO_HELP = 0xE3, //  Help key on ICO,
        ICO_00 = 0xE4, //  00 key on ICO,
        PROCESSKEY = 0xE5,
        ICO_CLEAR = 0xE6,
        PACKET = 0xE7,
        OEM_RESET = 0xE9,
        OEM_JUMP = 0xEA,
        OEM_PA1 = 0xEB,
        OEM_PA2 = 0xEC,
        OEM_PA3 = 0xED,
        OEM_WSCTRL = 0xEE,
        OEM_CUSEL = 0xEF,
        OEM_ATTN = 0xF0,
        OEM_FINISH = 0xF1,
        OEM_COPY = 0xF2,
        OEM_AUTO = 0xF3,
        OEM_ENLW = 0xF4,
        OEM_BACKTAB = 0xF5,
        ATTN = 0xF6,
        CRSEL = 0xF7,
        EXSEL = 0xF8,
        EREOF = 0xF9,
        PLAY = 0xFA,
        ZOOM = 0xFB,
        NONAME = 0xFC,
        PA1 = 0xFD,
        OEM_CLEAR = 0xFE,
        A = 0x41,
        B = 0x42,
        C = 0x43,
        D = 0x44,
        E = 0x45,
        F = 0x46,
        G = 0x47,
        H = 0x48,
        I = 0x49,
        J = 0x4a,
        K = 0x4b,
        L = 0x4c,
        M = 0x4d,
        N = 0x4e,
        O = 0x4f,
        P = 0x50,
        Q = 0x51,
        R = 0x52,
        S = 0x53,
        T = 0x54,
        U = 0x55,
        V = 0x56,
        W = 0x57,
        X = 0x58,
        Y = 0x59,
        Z = 0x5a,
        D0 = 0x30,
        D1 = 0x31,
        D2 = 0x32,
        D3 = 0x33,
        D4 = 0x34,
        D5 = 0x35,
        D6 = 0x36,
        D7 = 0x37,
        D8 = 0x38,
        D9 = 0x39
    }

    public enum ScanCodes
    {
        LBUTTON = 0,
        RBUTTON = 0,
        CANCEL = 70,
        MBUTTON = 0,
        XBUTTON1 = 0,
        XBUTTON2 = 0,
        BACK = 14,
        TAB = 15,
        CLEAR = 76,
        RETURN = 28,
        SHIFT = 42,
        CONTROL = 29,
        MENU = 56,
        PAUSE = 0,
        CAPITAL = 58,
        KANA = 0,
        HANGUL = 0,
        JUNJA = 0,
        FINAL = 0,
        HANJA = 0,
        KANJI = 0,
        ESCAPE = 1,
        CONVERT = 0,
        NONCONVERT = 0,
        ACCEPT = 0,
        MODECHANGE = 0,
        SPACE = 57,
        PRIOR = 73,
        NEXT = 81,
        END = 79,
        HOME = 71,
        LEFT = 75,
        UP = 72,
        RIGHT = 77,
        DOWN = 80,
        SELECT = 0,
        PRINT = 0,
        EXECUTE = 0,
        SNAPSHOT = 84,
        INSERT = 82,
        DELETE = 83,
        HELP = 99,
        KEY_0 = 11,
        KEY_1 = 2,
        KEY_2 = 3,
        KEY_3 = 4,
        KEY_4 = 5,
        KEY_5 = 6,
        KEY_6 = 7,
        KEY_7 = 8,
        KEY_8 = 9,
        KEY_9 = 10,
        KEY_A = 30,
        KEY_B = 48,
        KEY_C = 46,
        KEY_D = 32,
        KEY_E = 18,
        KEY_F = 33,
        KEY_G = 34,
        KEY_H = 35,
        KEY_I = 23,
        KEY_J = 36,
        KEY_K = 37,
        KEY_L = 38,
        KEY_M = 50,
        KEY_N = 49,
        KEY_O = 24,
        KEY_P = 25,
        KEY_Q = 16,
        KEY_R = 19,
        KEY_S = 31,
        KEY_T = 20,
        KEY_U = 22,
        KEY_V = 47,
        KEY_W = 17,
        KEY_X = 45,
        KEY_Y = 21,
        KEY_Z = 44,
        LWIN = 91,
        RWIN = 92,
        APPS = 93,
        SLEEP = 95,
        NUMPAD0 = 82,
        NUMPAD1 = 79,
        NUMPAD2 = 80,
        NUMPAD3 = 81,
        NUMPAD4 = 75,
        NUMPAD5 = 76,
        NUMPAD6 = 77,
        NUMPAD7 = 71,
        NUMPAD8 = 72,
        NUMPAD9 = 73,
        MULTIPLY = 55,
        ADD = 78,
        SEPARATOR = 0,
        SUBTRACT = 74,
        DECIMAL = 83,
        DIVIDE = 53,
        F1 = 59,
        F2 = 60,
        F3 = 61,
        F4 = 62,
        F5 = 63,
        F6 = 64,
        F7 = 65,
        F8 = 66,
        F9 = 67,
        F10 = 68,
        F11 = 87,
        F12 = 88,
        F13 = 100,
        F14 = 101,
        F15 = 102,
        F16 = 103,
        F17 = 104,
        F18 = 105,
        F19 = 106,
        F20 = 107,
        F21 = 108,
        F22 = 109,
        F23 = 110,
        F24 = 118,
        NUMLOCK = 69,
        SCROLL = 70,
        LSHIFT = 42,
        RSHIFT = 54,
        LCONTROL = 29,
        RCONTROL = 29,
        LMENU = 56,
        RMENU = 56,
        BROWSER_BACK = 106,
        BROWSER_FORWARD = 105,
        BROWSER_REFRESH = 103,
        BROWSER_STOP = 104,
        BROWSER_SEARCH = 101,
        BROWSER_FAVORITES = 102,
        BROWSER_HOME = 50,
        VOLUME_MUTE = 32,
        VOLUME_DOWN = 46,
        VOLUME_UP = 48,
        MEDIA_NEXT_TRACK = 25,
        MEDIA_PREV_TRACK = 16,
        MEDIA_STOP = 36,
        MEDIA_PLAY_PAUSE = 34,
        LAUNCH_MAIL = 108,
        LAUNCH_MEDIA_SELECT = 109,
        LAUNCH_APP1 = 107,
        LAUNCH_APP2 = 33,
        OEM_1 = 39,
        OEM_PLUS = 13,
        OEM_COMMA = 51,
        OEM_MINUS = 12,
        OEM_PERIOD = 52,
        OEM_2 = 53,
        OEM_3 = 41,
        OEM_4 = 26,
        OEM_5 = 43,
        OEM_6 = 27,
        OEM_7 = 40,
        OEM_8 = 0,
        OEM_102 = 86,
        PROCESSKEY = 0,
        PACKET = 0,
        ATTN = 0,
        CRSEL = 0,
        EXSEL = 0,
        EREOF = 93,
        PLAY = 0,
        ZOOM = 98,
        NONAME = 0,
        PA1 = 0,
        OEM_CLEAR = 0
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HardwareInput
    {
        public uint Message;
        public ushort Low;
        public ushort High;

        public uint WParam
        {
            get => ((uint)this.High << 16) | this.Low;
            set
            {
                this.Low = (ushort)value;
                this.High = (ushort)(value >> 16);
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseInput
    {
        public int X;
        public int Y;
        public uint Data;
        public MouseInputFlags Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardInput
    {
        public ushort VirtualKeyCode;
        public ushort ScanCode;
        public KeyboardInputFlags Flags;
        public uint Time;
        public IntPtr ExtraInfo;

        public VirtualKey Key
        {
            get => (VirtualKey)this.VirtualKeyCode;
            set => this.VirtualKeyCode = (ushort)value;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputPacket
    {
        [FieldOffset(0)] public MouseInput MouseInput;
        [FieldOffset(0)] public KeyboardInput KeyboardInput;
        [FieldOffset(0)] public HardwareInput HardwareInput;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Input
    {
        public InputType Type;
        public InputPacket Packet;

        public static void InitHardwareInput(out Input input, uint message, ushort low, ushort high)
        {
            input = new Input
            {
                Type = InputType.HARDWARE,
                Packet =
                {
                    HardwareInput =
                    {
                        Message = message,
                        Low = low,
                        High = high
                    }
                }
            };
        }

        public static void InitHardwareInput(out Input input, uint message, uint wParam)
        {
            InitHardwareInput(out input, message, (ushort)wParam, (ushort)(wParam >> 16));
        }

        public static void InitKeyboardInput(out Input input, VirtualKey key, bool isKeyUp, uint timestampMillis = 0)
        {
            input = new Input
            {
                Type = InputType.KEYBOARD,
                Packet =
                {
                    KeyboardInput =
                    {
                        Time = timestampMillis,
                        Key = key,
                        ScanCode = 0,
                        Flags = 0
                    }
                }
            };
            if (isKeyUp)
                input.Packet.KeyboardInput.Flags |= KeyboardInputFlags.KEYUP;
        }

        public static void InitKeyboardInput(out Input input, ushort scanCode, bool isKeyUp, bool isExtendedKey = false, uint timestampMillis = 0)
        {
            input = new Input
            {
                Type = InputType.KEYBOARD,
                Packet =
                {
                    KeyboardInput =
                    {
                        Time = timestampMillis,
                        Flags = KeyboardInputFlags.SCANCODE,
                        ScanCode = scanCode,
                        VirtualKeyCode = 0
                    }
                }
            };
            if (isKeyUp)
                input.Packet.KeyboardInput.Flags |= KeyboardInputFlags.KEYUP;
            if (isExtendedKey)
                input.Packet.KeyboardInput.Flags |= KeyboardInputFlags.EXTENDED;
        }

        public static void InitKeyboardInput(out Input input, char charCode, bool isKeyUp, uint timestampMillis = 0)
        {
            input = new Input
            {
                Type = InputType.KEYBOARD,
                Packet =
                {
                    KeyboardInput =
                    {
                        Time = timestampMillis,
                        Flags = KeyboardInputFlags.UNICODE,
                        ScanCode = charCode,
                        VirtualKeyCode = 0
                    }
                }
            };
            if (isKeyUp)
                input.Packet.KeyboardInput.Flags |= KeyboardInputFlags.KEYUP;
        }

        public static void InitMouseInput(out Input input, int x, int y, MouseInputFlags flags, uint data = 0, uint timestampMillis = 0)
        {
            input = new Input
            {
                Type = InputType.MOUSE,
                Packet =
                {
                    MouseInput =
                    {
                        Time = timestampMillis,
                        X = x,
                        Y = y,
                        Data = data,
                        Flags = flags
                    }
                }
            };
        }
    }
}
