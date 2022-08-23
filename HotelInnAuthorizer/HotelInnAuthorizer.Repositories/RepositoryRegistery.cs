using HotelInnAuthorizer.Repositories.Interfaces;
using HotelInnAuthorizer.Repositories.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelInnAuthorizer.Repositories
{
    public static class RepositoryRegistery
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            return services;
        }
    }
}
