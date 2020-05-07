using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities.Companies;

namespace Infrastructure.Repositories.Companies
{
    public class InMemoryCompany
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PaycheckRate { get; set; }
        public List<InMemoryEmployee> Employees { get; } = new List<InMemoryEmployee>();
    }
    
    public static partial class CompanyMappingExtensions
    {
        public static Company ToCompany(this InMemoryCompany inMemoryCompany)
        {
            var company = new Company(
                inMemoryCompany.Id,
                inMemoryCompany.Name, 
                inMemoryCompany.PaycheckRate, 
                inMemoryCompany.Employees.Select(inMemoryEmployee => inMemoryEmployee.ToEmployee()));

            return company;
        }

        public static InMemoryCompany ToInMemoryCompany(this Company company)
        {
            var inMemoryCompany = new InMemoryCompany
            {
                Id = company.Id,
                Name = company.Name,
                PaycheckRate = company.PayPeriod.Id,
            };
            
            inMemoryCompany.Employees.AddRange(company.Employees.Select(employee => employee.ToInMemoryEmployee()));

            return inMemoryCompany;
        }
    }
}