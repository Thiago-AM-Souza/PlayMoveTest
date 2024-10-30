using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Fornecedores.API.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFornecedorApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer()
                    .AddCarter()
                    .AddSwaggerServices()
                    .AddSecurityServices(configuration);

            return services;
        }

        private static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSecret = configuration["JWT_SECRET"];

            var key = Encoding.ASCII.GetBytes(jwtSecret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Fornecedor", policy => policy.RequireRole("Admin"));
            });

            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fornecedores", Version = "v1" });
                c.DocInclusionPredicate((_, api) => !string.IsNullOrEmpty(api.GroupName));

                c.TagActionsBy(api =>
                {
                    var endpointGroupName = api.ActionDescriptor.EndpointMetadata
                        .OfType<IEndpointGroupNameMetadata>()
                        .FirstOrDefault()?.EndpointGroupName;

                    return endpointGroupName != null ? new[] { endpointGroupName } : new[] { "Outros" };
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Por favor, insira 'Bearer ' + o token para testar a API.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
