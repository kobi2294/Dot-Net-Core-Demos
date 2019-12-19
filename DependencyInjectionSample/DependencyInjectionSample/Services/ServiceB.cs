using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Services
{
    public class ServiceB: IQueryAware
    {
        public Guid Guid { get; set; }

        public ServiceB()
        {
            Guid = Guid.NewGuid();
        }

        public void QueryPerformed(string query)
        {
            Console.WriteLine($"Service B {Guid} Is aware of query: {query}");            
        }
    }
}
