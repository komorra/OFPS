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
        public Matrix World;
        public Matrix View;
        public Matrix Projection;
        public Texture2DResource DiffuseTexture;
        public Texture2DResource Lightmap;
    }
}
