using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.People;
using Infrastructure.Proxies.People.Requests;
using Infrastructure.Repositories.People;
using MediatR;

namespace Infrastructure.Proxies.People.Handlers
{
    public class GetPersonHandler : IRequestHandler<GetPerson, Person>
    {
        private IPeopleRepository _peopleRepository { get; }

        public GetPersonHandler(IPeopleRepository peopleRepository) =>
            (_peopleRepository) = (peopleRepository);
        
        public async Task<Person> Handle(GetPerson request, CancellationToken cancellationToken)
        {
            var person = await _peopleRepository.LookupPerson(request.TaxIdentificationNumber);
            return person;
        }
    }
}