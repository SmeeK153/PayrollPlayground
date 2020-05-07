using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Entities.Companies
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> LookupCompanies();
        
        Task<Company> LookupCompany(Guid id);
        
        Task<Company> CreateNewCompany(string name, int paychecksPerYear);
    }
}