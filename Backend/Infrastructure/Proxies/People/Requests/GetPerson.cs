using Domain.Entities.People;
using Domain.ValueObjects;
using MediatR;

namespace Infrastructure.Proxies.People.Requests
{
    public class GetPerson : IRequest<Person>
    {
        public Name Name { get; set; }
        public long TaxIdentificationNumber { get; set; }
    }
}