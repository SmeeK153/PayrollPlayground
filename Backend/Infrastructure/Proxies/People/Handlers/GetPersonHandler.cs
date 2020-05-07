using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.People;
using Infrastructure.Proxies.People.Requests;
using MediatR;

namespace Infrastructure.Proxies.People.Handlers
{
    public class GetPersonHandler : IRequestHandler<GetOrCreatePerson, Person>
    {
        private IPeopleRepository _peopleRepository { get; }

        public GetPersonHandler(IPeopleRepository peopleRepository) =>
            (_peopleRepository) = (peopleRepository);
        
        public async Task<Person> Handle(GetOrCreatePerson request, CancellationToken cancellationToken)
        {
            var person = await _peopleRepository.LookupPerson(request.TaxIdentificationNumber);
            if (person is null)
            {
                person = await _peopleRepository.CreateNewPerson(request.Name, request.TaxIdentificationNumber);
            }
            return person;
        }
    }
}