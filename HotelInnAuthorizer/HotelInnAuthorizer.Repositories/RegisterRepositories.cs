using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelInnAuthorizer.Repositories
{
    public static class RegisterRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthorizerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Database")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            }).AddEntityFrameworkStores<AuthorizerDbContext>();
            return services;
        }
    }
}
