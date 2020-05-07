using System.Threading.Tasks;
using MediatR;

namespace Infrastructure.Proxies
{
    public interface IRequestService
    {
        Task<TResponse> Execute<TResponse>(IRequest<TResponse> query);
    }
}