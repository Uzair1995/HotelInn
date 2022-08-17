using HotelInn.Domain.IRepositories;
using HotelInn.Domain.Models;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace HotelInn.Persistence.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelInnDbContext hotelInnDbContext;

        public HotelRepository(HotelInnDbContext hotelInnDbContext)
        {
            this.hotelInnDbContext = hotelInnDbContext;
        }

        public async Task AddOrUpdateHotelAsync(Hotel hotel)
        {
            hotel.LastUpdateTime = DateTime.Now;
            var alreadySavedData = await hotelInnDbContext.Hotels.FindAsync(hotel.HotelId);

            if (alreadySavedData == null)
            {
                hotel.SaveDateTime = DateTime.Now;
                hotelInnDbContext.Hotels.Add(hotel);
            }
            else
            {
                foreach (PropertyInfo info in hotel.GetType().GetProperties())
                {
                    info.SetValue(alreadySavedData, info.GetValue(hotel));
                }
                hotelInnDbContext.Hotels.Update(alreadySavedData);
            }

            await hotelInnDbContext.SaveChangesAsync();
        }

        public async Task DeleteHotelAsync(string hotelId)
        {
            var alreadySavedData = await hotelInnDbContext.Hotels.FindAsync(hotelId);
            if (alreadySavedData != null)
            {
                hotelInnDbContext.Hotels.Remove(alreadySavedData);
                await hotelInnDbContext.SaveChangesAsync();
            }
        }

        public async Task<Hotel> FindHotelAsync(string hotelId)
        {
            return await hotelInnDbContext.Hotels.FindAsync(hotelId);
        }
    }
}
