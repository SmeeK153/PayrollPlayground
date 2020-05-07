using System.Collections.Generic;
using Domain.Entities.Companies;
using MediatR;

namespace Infrastructure.Proxies.Companies.Requests
{
    public class GetAllCompanies : IRequest<IEnumerable<Company>>
    {
    }
}