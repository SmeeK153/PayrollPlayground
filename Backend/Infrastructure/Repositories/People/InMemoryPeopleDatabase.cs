using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.People;
using Domain.ValueObjects;

namespace Infrastructure.Repositories.People
{
    public class InMemoryPeopleDatabase : IPeopleRepository
    {
        private Dictionary<long, InMemoryPerson> _peopleCatalog { get; } = new Dictionary<long, InMemoryPerson>();

        public Task<IEnumerable<Person>> LookupPeople()
        {
            var people = _peopleCatalog.Values.Select(p => p.ToPerson());
            return Task.FromResult(people);
        }

        public Task<Person> LookupPerson(long taxIdentificationNumber)
        {
            _peopleCatalog.TryGetValue(taxIdentificationNumber, out InMemoryPerson inMemoryPerson);
            if (inMemoryPerson is null)
            {
                return Task.FromResult<Person>(null);
            }
            
            var person = inMemoryPerson.ToPerson();
            return Task.FromResult(person);
        }

        public Task<Person> CreateNewPerson(Name name, long taxIdentification)
        {
            var inMemoryPerson = new InMemoryPerson
            {
                Id = Guid.NewGuid(),
                FirstName = name.First,
                LastName = name.Last
            };
            _peopleCatalog.Add(taxIdentification, inMemoryPerson);
            var person = inMemoryPerson.ToPerson();
            return Task.FromResult(person);
        }
    }
}