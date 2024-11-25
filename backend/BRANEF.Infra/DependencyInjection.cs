using BRANEF.Domain.Interfaces.IRepository;
using BRANEF.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BRANEF.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();

            return services;
        }
    }
}