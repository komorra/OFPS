using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OFPSEngine.Rendering;
using SharpDX.Direct3D11;

namespace OFPSEngine.ResourceManagement
{
    public class Texture2DLoader : IEngineResourceLoader<Texture2DResource>
    {
        public Texture2DResource Load(Stream assetStream, int size)
        {
            var resource = new Texture2DResource(Resource.FromStream<Texture2D>(Renderer.Current.Device, assetStream, size));
            return resource;
        }
    }
}
