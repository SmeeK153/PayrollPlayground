using Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static partial class CorsExtensions
    {
        public static IServiceCollection AddCorsSupport(this IServiceCollection services, IConfiguration configuration)
        {
            var corsConfiguration = configuration.GetSection("Cors").Get<CorsConfiguration>();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    // .AllowAnyHeader()
                    // .AllowAnyMethod()
                    // .AllowAnyMethod()

                }); 
            });

            return services;
        }
    }
}