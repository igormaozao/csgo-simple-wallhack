using SharpDX.Mathematics.Interop;
using System.Drawing;

namespace CGRedSoft.Classes {
    public static class Extensions {
        public static RawColor4 ToRaw(this Color col) {
            return new RawColor4(col.R / 255f, col.G / 255f, col.B / 255f, col.A / 255f);
        }
    }
}
