using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Services
{
    public class MetadataService : IMetadataService, IMetadataWriter
    {
        public Guid RequestId { get; private set; }
        public string Accept { get; private set; }

        public string UserAgent { get; private set; }

        public string Host { get; private set; }

        public int PeopleCount { get; private set; }

        public void Write(string accept, string userAgent, string host, int peopleCount)
        {
            Accept = accept;
            UserAgent = userAgent;
            Host = host;
            PeopleCount = peopleCount;
            RequestId = Guid.NewGuid();
        }
    }
}
