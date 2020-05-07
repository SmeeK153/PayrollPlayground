using System;
using Domain.Entities.Companies;
using MediatR;

namespace Infrastructure.Proxies.Companies.Requests
{
    public class GetCompany : IRequest<Company>
    {
        public Guid Id { get; set; }
    }
}