using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OFPSEngine.ResourceManagement
{
    public interface IEngineResourceLoader<out T> where T : EngineResource
    {
        T Load(Stream assetStream, int size);
    }
}
