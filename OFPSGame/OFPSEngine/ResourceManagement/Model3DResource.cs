using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assimp;

namespace OFPSEngine.ResourceManagement
{
    public class Model3DResource : EngineResource
    {
        private Scene scene;

        internal Model3DResource(Scene scene)
        {
            this.scene = scene;
        }
    }
}
