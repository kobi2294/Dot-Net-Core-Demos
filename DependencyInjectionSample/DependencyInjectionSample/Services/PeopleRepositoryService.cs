using DependencyInjectionSample.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Services
{
    public class PeopleRepositoryService : IPeopleRepositoryService
    {
        private const string _filePath = "Models/people.json";
        private IServiceProvider _serviceProvider;
        private Lazy<Task<Person[]>> _loader;

        public Guid Id { get; private set; }

        public PeopleRepositoryService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _loader = new Lazy<Task<Person[]>>(_load);
            Id = Guid.NewGuid();
        }

        private async Task<Person[]> _load()
        {
            var myText = await File.ReadAllTextAsync(_filePath);
            var data = JsonConvert.DeserializeObject<Person[]>(myText);
            return data;
        }

        private Task<Person[]> _getAll()
        {
            return _loader.Value;
        }

        public Task<Person[]> GetAllPeople()
        {
            var queryAwares = _serviceProvider.GetServices<IQueryAware>();
            foreach (var qa in queryAwares)
            {
                qa.QueryPerformed("Get All People");
            }
            return _getAll();
        }
    }
}
