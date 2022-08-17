using HotelInn.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelInn.Domain.IRepositories
{
    public interface IHotelRepository
    {
        Task AddOrUpdateHotelAsync(Hotel hotel);
        Task<Hotel> FindHotelAsync(string hotelId);
        Task DeleteHotelAsync(string hotelId);
        Task<List<Hotel>> SearchHotelsAndListAsync(Contracts.Hotel.SearchHotel searchHotel);
    }
}
