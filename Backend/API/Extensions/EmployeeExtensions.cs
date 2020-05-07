using System.Linq;
using API.Controllers.Companies.v1.Responses;
using Domain.Entities.Employees;
using Domain.Enumerations;

namespace API.Extensions
{
    public static partial class EmployeeExtensions
    {
        public static EmployeeSummary ToSummary(this Employee employee)
        {
            return new EmployeeSummary
            {
                Id = employee.Id,
                Name = employee.Person.Name.ToString()
            };
        }

        public static EmployeeDetail ToDetail(this Employee employee)
        {
            var salary = employee.CompanySalary.ConvertTo(Duration.Annual).AmountInCents;
            var benefitExpenses = employee.GetLineItemDeductions();
            var employeeDetail = new EmployeeDetail
            {
                Id = employee.Id,
                Name = employee.Person.Name.ToString(),
                Deductions = benefitExpenses,
                Dependents = employee.Dependents.Select(dependent => dependent.Person.ToString()),
                NetPay = salary - benefitExpenses.Values.Sum(),
                Salary = salary
            };
            return employeeDetail;
        }
    }
}