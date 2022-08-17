using HotelInn.Domain.IRepositories;
using HotelInn.Domain.Models;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace HotelInn.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelInnDbContext hotelInnDbContext;

        public BookingRepository(HotelInnDbContext hotelInnDbContext)
        {
            this.hotelInnDbContext = hotelInnDbContext;
        }

        public async Task AddOrUpdateBookingAsync(Booking booking)
        {
            booking.LastUpdateTime = DateTime.Now;
            var alreadySavedData = await hotelInnDbContext.Bookings.FindAsync(booking.BookingId);

            if (alreadySavedData == null)
            {
                booking.SaveDateTime = DateTime.Now;
                hotelInnDbContext.Bookings.Add(booking);
            }
            else
            {
                foreach (PropertyInfo info in booking.GetType().GetProperties())
                {
                    info.SetValue(alreadySavedData, info.GetValue(booking));
                }
                hotelInnDbContext.Bookings.Update(alreadySavedData);
            }

            await hotelInnDbContext.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(string bookingId)
        {
            var alreadySavedData = await hotelInnDbContext.Bookings.FindAsync(bookingId);
            if (alreadySavedData != null)
            {
                hotelInnDbContext.Bookings.Remove(alreadySavedData);
                await hotelInnDbContext.SaveChangesAsync();
            }
        }

        public async Task<Booking> FindBookingAsync(string bookingId)
        {
            return await hotelInnDbContext.Bookings.FindAsync(bookingId);
        }
    }
}
