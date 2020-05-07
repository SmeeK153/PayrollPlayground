using System;
using Domain.Entities.People;
using Domain.ValueObjects;

namespace Infrastructure.Repositories.People
{
    public class InMemoryPerson
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public static partial class PersonMappingExtensions
    {
        public static Person ToPerson(this InMemoryPerson inMemoryPerson)
        {
            var person = new Person(inMemoryPerson.Id, new Name(inMemoryPerson.FirstName, inMemoryPerson.LastName));
            return person;
        }

        public static InMemoryPerson ToInMemoryPerson(this Person person)
        {
            var inMemoryPerson = new InMemoryPerson
            {
                Id = person.Id,
                FirstName = person.Name.First,
                LastName = person.Name.Last
            };
            return inMemoryPerson;
        }
    }
}