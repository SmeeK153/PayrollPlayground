using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.People;
using Domain.ValueObjects;

namespace Infrastructure.Repositories.People
{
    public class InMemoryPeopleDatabase : IPeopleRepository
    {
        private Dictionary<long, Person> _peopleCatalog { get; } = new Dictionary<long, Person>();

        public IReadOnlyCollection<Person> People => _peopleCatalog.Values;

        public Task<Person> LookupPerson(long taxIdentificationNumber)
        {
            _peopleCatalog.TryGetValue(taxIdentificationNumber, out Person person);
            return Task.FromResult(person);
        }

        public Task<Person> CreateNewPerson(Name name, long taxIdentification)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public Task DeletePerson(Guid person)
        {
            throw new NotImplementedException();
        }
    }
}