using HotelInnAuthorizer.Services.Interfaces;
using HotelInnAuthorizer.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelInnAuthorizer.Services
{
    public static class ServicesRegistery
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAccountService, AccountService>();
            return services;
        }
    }
}
