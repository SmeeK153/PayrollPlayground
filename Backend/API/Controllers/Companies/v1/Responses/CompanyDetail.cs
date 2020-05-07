using System.Collections.Generic;

namespace API.Controllers.Companies.v1.Responses
{
    public class CompanyDetail : CompanySummary
    {
        public IEnumerable<EmployeeSummary> Employees { get; set; }
    }
}