using System.Linq;
using API.Controllers.Companies.v1.Responses;
using Domain.Entities.Companies;

namespace API.Extensions
{
    public static partial class CompanyExtensions
    {
        public static CompanySummary ToSummary(this Company company)
        {
            return new CompanySummary
            {
                Id = company.Id,
                Name = company.Name
            };
        }

        public static CompanyDetail ToDetail(this Company company)
        {
            var companyDetail = new CompanyDetail
            {
                Id = company.Id,
                Name = company.Name,
                Employees = company.Employees.Select(employee => employee.ToSummary())
            };
            return companyDetail;
        }
    }
}