using Infrastructure.Proxies;
using Infrastructure.Repositories.Companies;
using Infrastructure.Repositories.People;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static partial class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IRequestService, MediatrProxyService>();
            services.AddSingleton<ICompanyRepository, InMemoryCompaniesDatabase>();
            services.AddSingleton<IPeopleRepository, InMemoryPeopleDatabase>();
            return services;
        }
    }
}