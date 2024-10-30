using Fornecedores.Application.Data;
using Fornecedores.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fornecedores.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFornecedorInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IFornecedorRepository, FornecedorRepository>();

            return services;
        }
    }
}
