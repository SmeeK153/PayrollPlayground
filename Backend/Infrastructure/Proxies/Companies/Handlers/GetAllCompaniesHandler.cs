using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Companies;
using Infrastructure.Proxies.Companies.Requests;
using MediatR;

namespace Infrastructure.Proxies.Companies.Handlers
{
    public class GetAllCompaniesHandler : IRequestHandler<GetAllCompanies, IEnumerable<Company>>
    {
        private ICompanyRepository _companyRepository { get; }

        public GetAllCompaniesHandler(ICompanyRepository companyRepository) =>
            (_companyRepository) = (companyRepository);
        
        public Task<IEnumerable<Company>> Handle(GetAllCompanies request, CancellationToken cancellationToken)
        {
            return _companyRepository.LookupCompanies();
        }
    }
}