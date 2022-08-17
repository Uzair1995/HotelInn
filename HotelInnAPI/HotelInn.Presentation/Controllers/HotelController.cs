using System;
using System.Threading.Tasks;
using HotelInn.Contracts.Hotel;
using HotelInn.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HotelInn.Presentation.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    [SwaggerTag("APIs to manage hotels.")]
    public class HotelController
    {
        private readonly Lazy<IHotelService> hotelService;

        public HotelController(Lazy<IHotelService> hotelService)
        {
            this.hotelService = hotelService;
        }

        [SwaggerOperation(Summary = "Get hotel details.")]
        [HttpGet]
        public async Task<Hotel> GetHotelDetailsAsync([FromQuery] string hotelId)
        {
            return await hotelService.Value.FindHotelAsync(hotelId);
        }

        [SwaggerOperation(Summary = "List hotels.")]
        [HttpGet("list")]
        public async Task<Hotel> GetHotelsListAsync([FromQuery] string hotelId)
        {
            return await hotelService.Value.FindHotelAsync(hotelId);
        }

        [SwaggerOperation(Summary = "Add a new hotel.")]
        [HttpPost]
        public async Task<string> AddNewHotelAsync(NewHotel hotel)
        {
            return await hotelService.Value.AddNewHotelAsync(hotel);
        }

        [SwaggerOperation(Summary = "Update values of a hotel.")]
        [HttpPut]
        public async Task<string> UpdateHotelAsync(Hotel hotel)
        {
            return await hotelService.Value.UpdateHotelAsync(hotel);
        }

        [SwaggerOperation(Summary = "Remove a hotel.")]
        [HttpDelete]
        public async Task<string> DeleteHotelAsync([FromQuery] string hotelId)
        {
            return await hotelService.Value.DeleteHotelAsync(hotelId);
        }
    }
}
