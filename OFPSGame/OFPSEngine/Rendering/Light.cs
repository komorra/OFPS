using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SharpDX;

namespace OFPSEngine.Rendering
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Light
    {
        public Vector3 Position;
        public Vector3 Direction;
        public Vector3 Color;
        public int Type;
    }
}
