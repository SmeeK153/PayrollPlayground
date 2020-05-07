using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Companies;
using Domain.Entities.Employees;

namespace Infrastructure.Repositories.Companies
{
    public interface ICompanyRepository
    {
        public IReadOnlyCollection<Company> Companies { get; }

        Task<Company> LookupCompany(Guid id);
        
        Task<Company> CreateNewCompany(string name, int paychecksPerYear);

        Task UpdateCompany(Company company);

        Task DeleteCompany(Guid company);

        Task AddEmployeeToCompany(Employee employee);
    }
}