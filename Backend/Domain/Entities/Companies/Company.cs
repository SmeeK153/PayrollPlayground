using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.Dependents;
using Domain.Entities.Employees;
using Domain.Entities.People;
using Domain.Enumerations;
using Domain.Events.Companies;
using Foundations.Core;

namespace Domain.Entities.Companies
{
    public class Company : Entity
    {
        public string Name { get; private set; }
        public Duration PayPeriod { get; private set; }
        public IEnumerable<Employee> Employees => _directory.Values;
        private Dictionary<Guid, Employee> _directory { get; }

        public Company(Guid id, string name, int paychecksPerYear, IEnumerable<Employee> employees) : base(id)
        {
            Name = name;
            PayPeriod = Enumeration.FromId<Duration>(paychecksPerYear);
            _directory = employees.ToDictionary(employee => employee.Id, employee => employee);
        }
        
        public Employee AddEmployee(Person person)
        {
            var employee = _directory.Values.FirstOrDefault(e => e.Person != person);
            if (employee is null)
            {
                employee = new Employee(Guid.NewGuid(), person, new List<Dependent>());
                _directory.Add(employee.Id, employee);
                
                PublishDomainEvent(new AddEmployeeEvent
                {
                    Company = Id,
                    Employee = employee
                });
                
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
                PublishDomainEvent(new RemoveEmployeeEvent
                {
                    Company = Id,
                    Employee = employee
                });
            }
        }

        public void CloseCompany()
        {
            PublishDomainEvent(new CloseCompanyEvent
            {
                Id = Id
            });
        }
    }
}