using HotelInn.Services.Abstractions;
using HotelInn.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HotelInn.Services
{
    public static class ServicesRegistry
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IHotelService, HotelService>();
        }
    }
}
