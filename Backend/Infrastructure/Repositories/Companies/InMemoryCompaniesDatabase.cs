using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Companies;
using Domain.Events.Companies;
using Infrastructure.Repositories.People;
using MediatR;

namespace Infrastructure.Repositories.Companies
{
    public class InMemoryCompaniesDatabase : 
        ICompanyRepository, 
        INotificationHandler<CloseCompanyEvent>, 
        INotificationHandler<AddEmployeeEvent>, 
        INotificationHandler<RemoveEmployeeEvent>,
        INotificationHandler<AddEmployeeDependent>,
        INotificationHandler<RemoveEmployeeDependent>
    {
        // private static readonly InMemoryCompany TestCompany = new InMemoryCompany
        // {
        //     Id = Guid.NewGuid(),
        //     Name = "Test Company",
        //     PaycheckRate = 26,
        //     Employees = { new InMemoryEmployee
        //     {
        //         Id = Guid.NewGuid(),
        //         Person = new InMemoryPerson
        //         {
        //             Id = Guid.NewGuid(),
        //             FirstName = "John",
        //             LastName = "Doe"
        //         }
        //     } }
        // };

        private static Dictionary<Guid, InMemoryCompany> _companyCatalog { get; } =
            new Dictionary<Guid, InMemoryCompany>();
        private static Dictionary<Guid, InMemoryEmployee> _employeeCatalog { get; } = 
            new Dictionary<Guid, InMemoryEmployee>();

        public Task<IEnumerable<Company>> LookupCompanies()
        {
            var companies = _companyCatalog.Values.Select(inMemoryCompany => inMemoryCompany.ToCompany());
            return Task.FromResult(companies);
        }
            

        public Task<Company> LookupCompany(Guid id)
        {
            _companyCatalog.TryGetValue(id, out InMemoryCompany inMemoryCompany);
            return Task.FromResult(inMemoryCompany.ToCompany());
        }

        public Task<Company> CreateNewCompany(string name, int paychecksPerYear)
        {
            var inMemoryCompany = new InMemoryCompany
            {
                Name = name,
                PaycheckRate = paychecksPerYear
            };
            _companyCatalog.Add(inMemoryCompany.Id, inMemoryCompany);
            
            var company = inMemoryCompany.ToCompany();
            return Task.FromResult(company);
        }

        public Task Handle(CloseCompanyEvent notification, CancellationToken cancellationToken)
        {
            _companyCatalog.Remove(notification.Id);
            return Task.CompletedTask;
        }

        public Task Handle(AddEmployeeEvent notification, CancellationToken cancellationToken)
        {
            if (_companyCatalog.TryGetValue(notification.Company, out InMemoryCompany inMemoryCompany))
            {
                var inMemoryEmployee = notification.Employee.ToInMemoryEmployee();
                inMemoryCompany.Employees.Add(inMemoryEmployee);
                _employeeCatalog.Add(inMemoryEmployee.Id, inMemoryEmployee);
            }

            return Task.CompletedTask;
        }

        public Task Handle(RemoveEmployeeEvent notification, CancellationToken cancellationToken)
        {
            if (_companyCatalog.TryGetValue(notification.Company, out InMemoryCompany inMemoryCompany))
            {
                inMemoryCompany.Employees.RemoveAll(x => x.Id == notification.Employee);
                _employeeCatalog.Remove(notification.Employee);
            }

            return Task.CompletedTask;
        }

        public Task Handle(AddEmployeeDependent notification, CancellationToken cancellationToken)
        {
            if (_employeeCatalog.TryGetValue(notification.Employee, out InMemoryEmployee inMemoryEmployee))
            {
                if (inMemoryEmployee.Dependents.All(d => d.Id != notification.Dependent.Id))
                {
                    inMemoryEmployee.Dependents.Add(notification.Dependent.ToInMemoryDependent());
                }
            }
            
            return Task.CompletedTask;
        }

        public Task Handle(RemoveEmployeeDependent notification, CancellationToken cancellationToken)
        {
            if (_employeeCatalog.TryGetValue(notification.Employee, out InMemoryEmployee inMemoryEmployee))
            {
                inMemoryEmployee.Dependents.RemoveAll(d => d.Id == notification.Dependent);
            }
            
            return Task.CompletedTask;
        }
    }
}