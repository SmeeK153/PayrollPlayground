using System;
using Domain.Entities.Employees;
using MediatR;

namespace Infrastructure.Proxies.Companies.Requests
{
    public class GetEmployee : IRequest<Employee>
    {
        public Guid Company { get; set; }
        public Guid Employee { get; set; }
    }
}