using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OFPSEngine.ResourceManagement
{
    public interface IEngineResourceResolver
    {
        Stream Resolve(string filename);
    }
}
