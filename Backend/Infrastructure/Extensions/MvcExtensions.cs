using Infrastructure.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Infrastructure.Extensions
{
    public static partial class MvcExtensions
    {
        public static IServiceCollection AddMvcServices(this IServiceCollection services)
        {
            services
                .AddMvc(options =>
                {
                    // Using 'UseMvc' to configure MVC is not supported while using Endpoint routing
                    // To continue using 'UseMvc', the setting 'MvcOptions.EnableEndpoiuntRouting = false' must be done
                    options.EnableEndpointRouting = false;
                    options.Filters.Add<ExceptionFilter>();
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);


            return services;
        }

        public static IApplicationBuilder UseMvcServices(this IApplicationBuilder builder)
        {
            builder.UseMvc();
            return builder;
        }
    }
}