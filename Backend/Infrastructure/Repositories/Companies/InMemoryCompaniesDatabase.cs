using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Companies;
using Domain.Entities.Employees;

namespace Infrastructure.Repositories.Companies
{
    public class InMemoryCompaniesDatabase : ICompanyRepository
    {
        private Dictionary<Guid, Company> _companyCatalog { get; } = new Dictionary<Guid, Company>();

        public IReadOnlyCollection<Company> Companies => _companyCatalog.Values;

        public Task<Company> LookupCompany(Guid id)
        {
            _companyCatalog.TryGetValue(id, out Company company);
            return Task.FromResult(company);
        }

        public Task<Company> CreateNewCompany(string name, int paychecksPerYear)
        {
            var company = new Company(name, paychecksPerYear);
            _companyCatalog.Add(company.Id, company);
            return Task.FromResult(company);
        }

        public Task UpdateCompany(Company company)
        {
            if (_companyCatalog.ContainsKey(company.Id))
            {
                
            }
            else
            {
                
            }

            return Task.CompletedTask;
        }

        public Task DeleteCompany(Guid company)
        {
            if (_companyCatalog.ContainsKey(company))
            {
                _companyCatalog.Remove(company);
            }

            return Task.CompletedTask;
        }

        public Task AddEmployeeToCompany(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}