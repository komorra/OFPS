using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OFPSEngine.ResourceManagement
{
    public class GenericFileResolver : IEngineResourceResolver
    {
        private string baseDirectory;

        public GenericFileResolver(string baseDirectory)
        {
            this.baseDirectory = baseDirectory;
        }

        public Stream Resolve(string filename, out int size)
        {
            var appDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var stream = File.OpenRead(Path.Combine(appDir, baseDirectory, filename));
            size = (int)stream.Length;
            return stream;                       
        }
    }
}
