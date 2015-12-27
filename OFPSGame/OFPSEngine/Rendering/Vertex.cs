using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace OFPSEngine.Rendering
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 Uv;
        public Vector3 Tangent;
        public Vector3 BiNormal;

        public Vertex(Vector3 position, Vector3 normal, Vector2 uv)
        {
            Position = position;
            Normal = normal;
            Uv = uv;   
            Tangent = Vector3.Zero;
            BiNormal = Vector3.Zero;            
        }

        public Vertex(Vector3 position, Vector3 normal, Vector2 uv, Vector3 tangent, Vector3 binormal)
        {
            Position = position;
            Normal = normal;
            Uv = uv;
            Tangent = tangent;
            BiNormal = binormal;
        }

        public const int Stride = sizeof(float) * (3 + 3 + 2 + 3 + 3);

        public static InputElement[] GetElements()
        {
            return new[]
            {
                new InputElement("Position",0, Format.R32G32B32_Float, 0, 0),
                new InputElement("Normal", 0, Format.R32G32B32_Float, sizeof(float)*3,0),
                new InputElement("Texcoord", 0, Format.R32G32_Float, sizeof(float)*6,0),
                new InputElement("Tangent", 0, Format.R32G32B32_Float, sizeof(float)*8,0),
                new InputElement("Binormal", 0, Format.R32G32B32_Float, sizeof(float)*11,0),
            };
        }
    }
}
