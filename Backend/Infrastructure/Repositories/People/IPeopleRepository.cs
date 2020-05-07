using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.People;
using Domain.ValueObjects;

namespace Infrastructure.Repositories.People
{
    public interface IPeopleRepository
    {
        public IReadOnlyCollection<Person> People { get; }

        Task<Person> LookupPerson(long taxIdentificationNumber);
        
        Task<Person> CreateNewPerson(Name name, long taxIdentification);

        Task UpdatePerson(Person person);

        Task DeletePerson(Guid person);
    }
}