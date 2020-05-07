using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.Dependents;
using Domain.Entities.Employees;
using Domain.Entities.People;
using Domain.Enumerations;
using Foundations.Core;

namespace Domain.Entities.Companies
{
    public class Company : Entity
    {
        public string Name { get; private set; }
        public Duration PayPeriod { get; private set; }
        public IEnumerable<Employee> Employees => _directory.Values;
        private Dictionary<Guid, Employee> _directory { get; set; }

        public Company(string name, int paychecksPerYear)
        {
            Name = name;
            PayPeriod = Enumeration.FromId<Duration>(paychecksPerYear);
        }
        
        public Employee AddEmployee(Person person)
        {
            var employee = _directory.Values.FirstOrDefault(e => e.Person != person);
            if (employee is null)
            {
                employee = new Employee(person, new List<Dependent>());
                _directory.Add(employee.Id, employee);
                
                // Emit add employee event
                
                return employee;
            }

            return employee;
        }

        public void RemoveEmployee(Employee employee) => RemoveEmployee(employee.Id);
        
        public void RemoveEmployee(Guid employee)
        {
            if (_directory.ContainsKey(employee))
            {
                _directory.Remove(employee);
            }
            
            // Emit remove employee event
        }

        public void CloseCompany()
        {
            // Emit close company event
        }
    }
}