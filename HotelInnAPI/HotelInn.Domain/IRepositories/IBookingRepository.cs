using HotelInn.Domain.Models;
using System.Threading.Tasks;

namespace HotelInn.Domain.IRepositories
{
    public interface IBookingRepository
    {
        Task AddOrUpdateBookingAsync(Booking booking);
        Task<Booking> FindBookingAsync(string bookingId);
        Task DeleteBookingAsync(string bookingId);
    }
}
