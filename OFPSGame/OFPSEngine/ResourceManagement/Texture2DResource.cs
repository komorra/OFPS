using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OFPSEngine.Rendering;
using SharpDX.Direct3D11;

namespace OFPSEngine.ResourceManagement
{
    public class Texture2DResource : EngineResource
    {
        internal Texture2D Texture { get; private set; }
        internal ShaderResourceView View { get; private set; }

        internal Texture2DResource(Texture2D texture)
        {
            Texture = texture;
            View = new ShaderResourceView(Renderer.Current.Device, Texture);
        }

        ~Texture2DResource()
        {
            View.Dispose();
            Texture.Dispose();
        }
    }
}
