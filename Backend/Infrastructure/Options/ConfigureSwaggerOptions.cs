using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Options
{
    // github.com/microsoft/aspnet-api-versioning/wiki/API-Documentation
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private IApiVersionDescriptionProvider _provider { get; }

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => (_provider) = (provider);
        
        public void Configure(SwaggerGenOptions options)
        {
            foreach ( var description in _provider.ApiVersionDescriptions )
            {
                options.SwaggerDoc(
                    description.GroupName,
                    new OpenApiInfo()
                    {
                        Title = $"Company Benefits API {description.GroupName}",
                        Version = description.ApiVersion.ToString(),
                    } );
            }
        }
    }
}