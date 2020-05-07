using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Companies;
using Infrastructure.Proxies.Companies.Requests;
using Infrastructure.Repositories.Companies;
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
            return Task.FromResult(_companyRepository.Companies.AsEnumerable());
        }
    }
}