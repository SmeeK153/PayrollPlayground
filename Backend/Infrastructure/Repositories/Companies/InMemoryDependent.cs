using System;
using Domain.Entities.Dependents;
using Infrastructure.Repositories.People;

namespace Infrastructure.Repositories.Companies
{
    public class InMemoryDependent
    {
        public Guid Id { get; set; }
        
        public InMemoryPerson Person { get; set; }
    }
    
    public static partial class EmployeeMappingExtensions
    {
        public static Dependent ToDependent(this InMemoryDependent inMemoryDependent)
        {
            var dependent = new Dependent(inMemoryDependent.Id, inMemoryDependent.Person.ToPerson());
            return dependent;
        }

        public static InMemoryDependent ToInMemoryDependent(this Dependent employee)
        {
            var inMemoryDependent = new InMemoryDependent
            {
                Id = employee.Id,
                Person = employee.Person.ToInMemoryPerson()
            };
            return inMemoryDependent;
        }
    }
}