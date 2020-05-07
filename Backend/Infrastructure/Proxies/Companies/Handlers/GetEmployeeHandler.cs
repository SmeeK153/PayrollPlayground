using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Companies;
using Domain.Entities.Employees;
using Infrastructure.Proxies.Companies.Requests;
using MediatR;

namespace Infrastructure.Proxies.Companies.Handlers
{
    public class GetEmployeeHandler : IRequestHandler<GetEmployee, Employee>
    {
        private ICompanyRepository _companyRepository { get; }

        public GetEmployeeHandler(ICompanyRepository companyRepository) =>
            (_companyRepository) = (companyRepository);

        public async Task<Employee> Handle(GetEmployee request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.LookupCompany(request.Company);
            var employee = company.Employees.FirstOrDefault(e => e.Id == request.Employee);
            return employee;
        }
    }
}