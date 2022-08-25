using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelInn.Contracts.Hotel;
using HotelInn.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [SwaggerOperation(Summary = "Get hotel details.")]
        [HttpGet]
        public async Task<Hotel> GetHotelDetailsAsync([FromQuery] string hotelId)
        {
            return await hotelService.Value.FindHotelAsync(hotelId);
        }

        [Authorize]
        [SwaggerOperation(Summary = "List hotels.")]
        [HttpGet("list")]
        public async Task<List<Hotel>> GetHotelsListAsync([FromQuery] SearchHotel searchHotel)
        {
            return await hotelService.Value.SearchHotels(searchHotel); ;
        }

        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Add a new hotel.")]
        [HttpPost]
        public async Task<string> AddNewHotelAsync(NewHotel hotel)
        {
            return await hotelService.Value.AddNewHotelAsync(hotel);
        }

        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update values of a hotel.")]
        [HttpPut]
        public async Task<string> UpdateHotelAsync(Hotel hotel)
        {
            return await hotelService.Value.UpdateHotelAsync(hotel);
        }

        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Remove a hotel.")]
        [HttpDelete]
        public async Task<string> DeleteHotelAsync([FromQuery] string hotelId)
        {
            return await hotelService.Value.DeleteHotelAsync(hotelId);
        }
    }
}
