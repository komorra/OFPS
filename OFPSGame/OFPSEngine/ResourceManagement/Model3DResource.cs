using Assimp;
using SharpDX.Direct3D11;

namespace OFPSEngine.ResourceManagement
{
    public class Model3DResource : EngineResource
    {
        internal Scene ModelData;
        internal Buffer VertexBuffer;
        internal Buffer IndexBuffer;
        public int VertexCount;
        public int IndexCount;

        internal Model3DResource(Scene modelData)
        {
            this.ModelData = modelData;
        }
    }
}
