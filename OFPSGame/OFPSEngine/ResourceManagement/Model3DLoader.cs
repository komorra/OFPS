using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assimp;

namespace OFPSEngine.ResourceManagement
{
    public class Model3DLoader : IEngineResourceLoader<Model3DResource>
    {
        private static AssimpContext importer;
        private static LogStream logStream;

        public Model3DResource Load(Stream assetStream, int size)
        {            
            if (importer == null)
            {
                importer = new AssimpContext();
                logStream = new LogStream((msg, data) =>
                {
                    Logger.Debug(msg);
                });
            }

            var model = importer.ImportFileFromStream(assetStream,
                PostProcessSteps.CalculateTangentSpace | PostProcessSteps.Triangulate | PostProcessSteps.FlipWindingOrder, "fbx");
            var resource = new Model3DResource(model);

            return resource;
        }
    }
}
