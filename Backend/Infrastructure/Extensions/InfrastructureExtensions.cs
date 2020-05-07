using Infrastructure.Proxies;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static partial class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IRequestService, MediatrProxyService>();
            return services;
        }
    }
}