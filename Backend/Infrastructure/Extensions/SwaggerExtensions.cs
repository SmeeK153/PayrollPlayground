using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Infrastructure.Configurations;
using Infrastructure.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Infrastructure.Extensions
{
    public static partial class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerInterface(this IServiceCollection services)
        {
            services
                .AddApiVersioning(options =>
                {
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                    options.UseApiBehavior = true;
                })
                .AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                })
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddSwaggerGen(options =>
                {
                    options.AddSecurityDefinition(
                        "Bearer", 
                        new OpenApiSecurityScheme
                        {
                            Description = "Authentication service token",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey
                        });

                    options.AddSecurityRequirement(
                        new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    },
                                    Scheme = "Bearer",
                                    Name = "Bearer_Scheme",
                                    In = ParameterLocation.Header
                                },
                                new List<string>()
                            }
                        });

                    var projectName = Assembly.GetCallingAssembly().GetName().Name;
                    var xml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", $"{projectName}.xml");
                    if (File.Exists(xml))
                    {
                        options.IncludeXmlComments(xml);
                    }
                });
            return services;
        }

        public static IApplicationBuilder UseSwaggerInterface(
            this IApplicationBuilder builder,
            IConfiguration configuration,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            var swaggerConfiguration = 
                configuration
                    .GetSection("Swagger")
                    .Get<SwaggerConfiguration>();

            builder
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.DocExpansion(DocExpansion.List);
                    // from https://github.com/domaindrivendev/Swashbuckle.AspNetCore#change-the-path-for-swagger-json-endpoints
                    // the default path for all documents is "/swagger/{documentation}/swagger/json"
                    apiVersionDescriptionProvider
                        .ApiVersionDescriptions.ToList()
                        .ForEach(descriptor =>
                        {
                            options.SwaggerEndpoint(
                                $"{swaggerConfiguration.BasePath}/swagger/{descriptor.GroupName}/swagger.json",
                                descriptor.GroupName);
                        });
                    options.RoutePrefix = string.Empty;
                    
                    options.OAuthClientId("");
                    options.OAuthClientSecret("");
                    options.OAuthRealm("");
                    options.OAuthAppName("");
                    options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                });
                // .UseJwtBearerAuthentication();

            return builder;
        }
    }
}