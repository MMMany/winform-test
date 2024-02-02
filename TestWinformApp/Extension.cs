using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestWinformApp.Internal
{
    public static class Extension
    {
        public static Point Center(this Rectangle rect) => new Point(rect.X + (int)(rect.Width / 2), rect.Y + (int)(rect.Height));
    }
}
