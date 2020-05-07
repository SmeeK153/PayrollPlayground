using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Domain.Entities.People
{
    public interface IPeopleRepository
    {
        Task<IEnumerable<Person>> LookupPeople();

        Task<Person> LookupPerson(long taxIdentificationNumber);
        
        Task<Person> CreateNewPerson(Name name, long taxIdentification);
    }
}