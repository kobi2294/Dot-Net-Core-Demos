using System;

namespace DependencyInjectionSample.Services
{
    public interface IMetadataService
    {
        Guid RequestId { get; }
        string Accept { get; }
        string Host { get; }
        int PeopleCount { get; }
        string UserAgent { get; }
    }
}