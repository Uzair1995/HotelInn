using HotelInn.Contracts.Booking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelInn.Services.Abstractions
{
    public interface IBookingService
    {
        Task<string> AddNewBookingAsync(NewBooking newBooking, string httpAccessToken);
        Task<string> UpdateBookingAsync(Booking booking, string httpAccessToken);
        Task<string> DeleteBookingAsync(string bookingId);
        Task<Booking> FindBookingAsync(string bookingId);
        Task<List<Booking>> FindUserBookingsAsync(string userId);
        Task<List<Booking>> FindHotelBookingsAsync(string hotelId);
    }
}