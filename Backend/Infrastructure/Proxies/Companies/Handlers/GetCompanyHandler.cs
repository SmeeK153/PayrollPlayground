using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Companies;
using Infrastructure.Proxies.Companies.Requests;
using Infrastructure.Repositories.Companies;
using MediatR;

namespace Infrastructure.Proxies.Companies.Handlers
{
    public class GetCompanyHandler : IRequestHandler<GetCompany, Company>
    {
        private ICompanyRepository _companyRepository { get; }

        public GetCompanyHandler(ICompanyRepository companyRepository) =>
            (_companyRepository) = (companyRepository);
        
        public async Task<Company> Handle(GetCompany request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.LookupCompany(request.Id);
            return company;
        }
    }
}