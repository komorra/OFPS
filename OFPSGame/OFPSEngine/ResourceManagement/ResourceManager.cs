using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OFPSEngine.ResourceManagement
{
    public class ResourceManager
    {
        private IEngineResourceResolver resolver;

        private Dictionary<string, IEngineResourceLoader<EngineResource>> loaders =
            new Dictionary<string, IEngineResourceLoader<EngineResource>>(); 

        public ResourceManager(IEngineResourceResolver resolver)
        {
            this.resolver = resolver;
        }

        public void RegisterLoader<T>(IEngineResourceLoader<T> loader, string filenameExtensions) where T : EngineResource
        {
            loaders.Add(filenameExtensions, loader);
        }

        public T Load<T>(string filename) where T : EngineResource
        {           
            var loader = loaders.FirstOrDefault(x => x.Key.Contains(Path.GetExtension(filename))).Value;
            var stream = resolver.Resolve(filename);
            var resource = loader.Load(stream) as T;
            stream.Dispose();

            return resource;
        }
    }
}
