using System.Collections.Generic;

namespace API.Controllers.Companies.v1.Responses
{
    public class EmployeeDetail : EmployeeSummary
    {
        public long Salary { get; set; }
        public Dictionary<string,long> Deductions { get; set; } = new Dictionary<string, long>();
        public long NetPay { get; set; }
        public IEnumerable<DependentSummary> Dependents { get; set; }
    }
}