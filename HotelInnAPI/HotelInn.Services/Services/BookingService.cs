using HotelInn.Contracts.Booking;
using HotelInn.Services.Abstractions;
using System.Threading.Tasks;

namespace HotelInn.Services.Services
{
    public class BookingService : IBookingService
    {
        public BookingService()
        {

        }

        public Task<string> AddNewBookingAsync(NewBooking newBooking)
        {
            
        }
    }
}
