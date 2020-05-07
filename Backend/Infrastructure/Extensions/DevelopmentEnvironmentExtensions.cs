using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace Infrastructure.Extensions
{
    public static partial class DevelopmentEnvironmentExtensions
    {
        public static IApplicationBuilder UseEnvironment(this IApplicationBuilder builder, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                builder.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }

            return builder;
        }
    }
}