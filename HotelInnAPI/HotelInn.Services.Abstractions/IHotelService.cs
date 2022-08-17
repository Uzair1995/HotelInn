using HotelInn.Contracts.Hotel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelInn.Services.Abstractions
{
    public interface IHotelService
    {
        Task<string> AddNewHotelAsync(Contracts.Hotel.NewHotel hotel);
        Task<string> UpdateHotelAsync(Contracts.Hotel.Hotel hotel);
        Task<Contracts.Hotel.Hotel> FindHotelAsync(string hotelId);
        Task<string> DeleteHotelAsync(string hotelId);
        Task<List<Hotel>> SearchHotels(SearchHotel searchHotel);
    }
}
