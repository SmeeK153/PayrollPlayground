using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.Employees;
using Infrastructure.Repositories.People;

namespace Infrastructure.Repositories.Companies
{
    public class InMemoryEmployee
    {
        public Guid Id { get; set; }
        public InMemoryPerson Person { get; set; }
        public List<InMemoryDependent> Dependents { get; set; } = new List<InMemoryDependent>();
    }
    
    public static partial class EmployeeMappingExtensions
    {
        public static Employee ToEmployee(this InMemoryEmployee inMemoryEmployee)
        {
            var employee = new Employee(
                inMemoryEmployee.Id,
                inMemoryEmployee.Person.ToPerson(), 
                inMemoryEmployee.Dependents.Select(d => d.ToDependent()));

            return employee;
        }

        public static InMemoryEmployee ToInMemoryEmployee(this Employee employee)
        {
            var inMemoryEmployee = new InMemoryEmployee
            {
                Id = employee.Id,
                Person = employee.Person.ToInMemoryPerson(),
                Dependents = employee.Dependents.Select(d => d.ToInMemoryDependent()).ToList()
            };
            return inMemoryEmployee;
        }
    }
}