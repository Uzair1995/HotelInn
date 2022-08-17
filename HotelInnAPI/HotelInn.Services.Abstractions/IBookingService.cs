using HotelInn.Contracts.Booking;
using System.Threading.Tasks;

namespace HotelInn.Services.Abstractions
{
    public interface IBookingService
    {
        Task<string> AddNewBookingAsync(NewBooking newBooking);
    }
}