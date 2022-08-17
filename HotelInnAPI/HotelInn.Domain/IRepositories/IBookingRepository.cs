using HotelInn.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelInn.Domain.IRepositories
{
    public interface IBookingRepository
    {
        Task AddOrUpdateBookingAsync(Booking booking);
        Task<Booking> FindBookingAsync(string bookingId);
        Task<List<Booking>> FindHotelBookingsAsync(string hotelId);
        Task<List<Booking>> FindUserBookingsAsync(string userId);
        Task DeleteBookingAsync(string bookingId);
    }
}
