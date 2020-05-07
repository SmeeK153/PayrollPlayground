using System.Threading.Tasks;
using MediatR;

namespace Infrastructure.Proxies
{
    public class MediatrProxyService : IRequestService
    {
        private IMediator _mediator { get; }
        
        public MediatrProxyService(IMediator mediator) => (_mediator) = (mediator);
        
        public async Task<TResponse> Execute<TResponse>(IRequest<TResponse> query) => await _mediator.Send(query);
        
    }
}