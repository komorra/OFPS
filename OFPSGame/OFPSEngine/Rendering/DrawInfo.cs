using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OFPSEngine.ResourceManagement;
using SharpDX;

namespace OFPSEngine.Rendering
{
    public class DrawInfo
    {
        public Vector3 CameraPosition;
        public Matrix World;
        public Matrix View;
        public Matrix Projection;
        public Texture2DResource DiffuseMap;
        public Texture2DResource LightMap;
        public Texture2DResource NormalMap;
        public Texture2DResource MetallicMap;
        public Texture2DResource RoughnessMap;
        public Texture2DResource CubeMap;
    }
}
