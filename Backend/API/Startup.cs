using Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddOptions();
            services.AddCorsSupport(Configuration);
            services.AddMvcServices();
            services.AddSwaggerInterface();
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddDomainEvents();
            services.AddInfrastructure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            app.UseEnvironment(env);
            app.UseHealthChecks("/health/live", new HealthCheckOptions());
            app.UseHealthChecks("/health/ready", new HealthCheckOptions());
            app.UseCors();
            app.UseAuthentication();
            app.UseSwaggerInterface(Configuration, apiVersionDescriptionProvider);
            app.UseStaticFiles();
            app.UseMvcServices();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseDomainEvents();
        }
    }
}