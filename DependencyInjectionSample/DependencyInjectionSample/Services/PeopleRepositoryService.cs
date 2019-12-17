using DependencyInjectionSample.Models;
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
        private Lazy<Task<Person[]>> _loader;

        public PeopleRepositoryService()
        {
            _loader = new Lazy<Task<Person[]>>(_load);
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
            return _getAll();
        }
    }
}
