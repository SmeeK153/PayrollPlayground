using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Companies;
using Infrastructure.Proxies.Companies.Requests;
using MediatR;

namespace Infrastructure.Proxies.Companies.Handlers
{
    public class CreateNewCompanyHandler : IRequestHandler<CreateNewCompany, Company>
    {
        private ICompanyRepository _companyRepository { get; }

        public CreateNewCompanyHandler(ICompanyRepository companyRepository) =>
            (_companyRepository) = (companyRepository);

        public async Task<Company> Handle(CreateNewCompany request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.CreateNewCompany(request.Name, request.PaychecksPerYear);
            return company;
        }
    }
}