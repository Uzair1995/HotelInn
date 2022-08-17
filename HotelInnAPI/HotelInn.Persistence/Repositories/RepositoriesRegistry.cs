using HotelInn.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelInn.Persistence.Repositories
{
    public static class RepositoriesRegistry
    {
        public static void RegisterRepositories(this IServiceCollection services, IConfiguration Configuration)
        {
            //Configuring the DB Context
            services.AddDbContext<HotelInnDbContext>(builder =>
            {
                var connectionString = Configuration.GetConnectionString("Database");
                builder.UseSqlServer(connectionString);
            });


            services.AddTransient<IHotelRepository, HotelRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
