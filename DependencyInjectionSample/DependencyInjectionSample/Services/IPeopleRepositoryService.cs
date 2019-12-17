using System;
using System.Threading.Tasks;
using DependencyInjectionSample.Models;

namespace DependencyInjectionSample.Services
{
    public interface IPeopleRepositoryService
    {
        Task<Person[]> GetAllPeople();

        public Guid Id { get; }
    }
}