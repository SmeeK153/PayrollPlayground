using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.Dependents;
using Domain.Entities.People;
using Domain.Events.Companies;
using Domain.ValueObjects;
using Domain.ValueObjects.Benefits;
using Foundations.Core;

namespace Domain.Entities.Employees
{
    public class Employee : Entity
    {
        public Person Person { get; private set; }
        public IEnumerable<Dependent> Dependents { get; private set; }
        
        public Salary CompanySalary { get; private set; }
        
        public CompanyBenefits Benefits { get; }

        public Employee(Guid id, Person person, IEnumerable<Dependent> dependents) : base(id)
        {
            Person = person;
            Benefits = new EmployeeBenefits(person);
            CompanySalary = new Salary(200000);
            Dependents = dependents;
        }

        public void AddDependent(Person person)
        {
            if (Dependents.All(preexistingDependent => preexistingDependent.Person.Id != person.Id))
            {
                var dependent = new Dependent(Guid.NewGuid(), person);
                Dependents.Append(dependent);

                PublishDomainEvent(new AddEmployeeDependent
                {
                    Employee = Id,
                    Dependent = dependent
                });
            }
        }

        public void RemoveDependent(Guid id)
        {
            if (Dependents.Any(preexistingDependent => preexistingDependent.Id == id))
            {
                Dependents = Dependents.Where(d => d.Id != id);

                PublishDomainEvent(new RemoveEmployeeDependent
                {
                    Employee = Id,
                    Dependent = id
                });
            }
        }
        
        public Dictionary<string, long> GetLineItemDeductions()
        {
            var lineItems = new Dictionary<string, long>
            {
                {"Company Benefits", Benefits.CalculateCost()}
            };
            foreach (Dependent dependent in Dependents)
            {
                lineItems.Add($"{dependent.Person.Name} Benefits", dependent.Benefits.CalculateCost());
            }
            return lineItems;
        }
    }
}