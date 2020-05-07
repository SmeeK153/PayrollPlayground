using System;
using System.Collections.Generic;
using Domain.Entities.Dependents;
using Domain.Entities.People;
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

        public Employee(Person person, IEnumerable<Dependent> dependents)
        {
            Person = person;
            Benefits = new EmployeeBenefits(person);
            CompanySalary = new Salary(200000);
            Dependents = dependents;
        }

        public void AddDependent(Dependent dependent)
        {
            
        }

        public void RemoveDependent(Guid id)
        {
            
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