using Domain.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Configuration
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwaggerSetup(this IServiceCollection services, AppSettingsConfig appSettings)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(s =>
            {
                var contact = new OpenApiContact
                {
                    Name = "ACT DIGITAL",
                    Email = "actdigital.com",
                    Url = new Uri("https://actdigital.com/pt")
                };

                var versions = appSettings.Application.Versions;

                foreach (var item in versions)
                {
                    s.SwaggerDoc(item.Version, new OpenApiInfo
                    {
                        Version = item.Version,
                        Title = "actdigital.com",
                        Description = "API de gerenciamento de funcionários",
                        Contact = contact
                    });
                }

                s.EnableAnnotations();

                // Configuração da autenticação personalizada do acesso à API interna
                s.AddSecurityDefinition("TokenDefault", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "TokenDefault",
                    Name = "Authorization",
                    Description = "Esse token será utilizado para a comunicação entre as APIs internas. Example: \"Authorization: Bearer {token}\""
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "TokenDefault"
                            },
                        },
                        new string[]{ }
                    }
                });

                s.OperationFilter<RemoveVersionFromParameter>();
                s.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                // Configurando para que o Swagger use os meus comentários
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "ActEmployeeManagementApi.xml");
                s.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app, AppSettingsConfig appSettings)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                var versions = appSettings.Application.Versions;

                foreach (var item in versions)
                {
                    s.SwaggerEndpoint($"/swagger/{item.Version}/swagger.json", $"API FAM versão: {item.Description}");
                }
            });
        }

        public class RemoveVersionFromParameter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var versionParameter = operation.Parameters.SingleOrDefault(p => p.Name == "version");
                if (versionParameter != null)
                {
                    operation.Parameters.Remove(versionParameter);
                }
            }
        }

        public class ReplaceVersionWithExactValueInPath : IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                var toReplaceWith = new OpenApiPaths();

                foreach (var (key, value) in swaggerDoc.Paths)
                {
                    toReplaceWith.Add(key.Replace("v{version}", swaggerDoc.Info.Version, StringComparison.InvariantCulture), value);
                }

                swaggerDoc.Paths = toReplaceWith;
            }
        }
    }
}

