using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Services
{
    public interface IMetadataWriter
    {
        void Write(string accept, string userAgent, string host, int peopleCount);
    }
}
