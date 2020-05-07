using Domain.Entities.Companies;
using MediatR;

namespace Infrastructure.Proxies.Companies.Requests
{
    public class CreateNewCompany : IRequest<Company>
    {
        public string Name { get; set; }
        public int PaychecksPerYear { get; set; }
    }
}