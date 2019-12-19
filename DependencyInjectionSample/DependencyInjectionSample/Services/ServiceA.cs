using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Services
{
    public class ServiceA: IQueryAware
    {
        public Guid Guid { get; set; }

        public ServiceA()
        {
            Guid = Guid.NewGuid();
        }

        public void QueryPerformed(string query)
        {
            Console.WriteLine($"Service A {Guid} Is aware of query: {query}");
        }
    }
}
