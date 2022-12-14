using HotelInn.Contracts.Booking;
using HotelInn.Presentation.Utils;
using HotelInn.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelInn.Presentation.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    [SwaggerTag("APIs to manage bookings.")]

    public class BookingController
    {
        private readonly Lazy<IBookingService> bookingService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BookingController(Lazy<IBookingService> bookingService, IHttpContextAccessor httpContextAccessor)
        {
            this.bookingService = bookingService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [SwaggerOperation(Summary = "Place a booking.")]
        [HttpPost]
        public async Task<string> AddNewBookingAsync(NewBooking newBooking)
        {
            var token = httpContextAccessor.HttpContext.GetAccessToken();
            return await bookingService.Value.AddNewBookingAsync(newBooking, token);
        }

        //TODO: verify if the booking belongs to this user. Only then send the details
        [Authorize]
        [SwaggerOperation(Summary = "Get a specific booking details.")]
        [HttpGet]
        public async Task<Booking> GetBookingDetailsAsync([FromQuery] string bookingId)
        {
            return await bookingService.Value.FindBookingAsync(bookingId);
        }

        [Authorize]
        [SwaggerOperation(Summary = "List all bookings of a specific user.")]
        [HttpGet("userbookings")]
        public async Task<List<Booking>> GetUserBookingsListAsync()
        {
            return await bookingService.Value.FindUserBookingsAsync(httpContextAccessor.HttpContext.User.GetUsername());
        }

        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "List all bookings of a specific hotel.")]
        [HttpGet("hotelbookings")]
        public async Task<List<Booking>> GetHotelBookingsListAsync([FromQuery] string hotelId)
        {
            return await bookingService.Value.FindHotelBookingsAsync(hotelId);
        }

        [Authorize]
        [SwaggerOperation(Summary = "Update values of a booking.")]
        [HttpPut]
        public async Task<string> UpdateBookingAsync(Booking booking)
        {
            var token = httpContextAccessor.HttpContext.GetAccessToken();
            return await bookingService.Value.UpdateBookingAsync(booking, token);
        }

        [Authorize]
        [SwaggerOperation(Summary = "Cancel a booking.")]
        [HttpDelete]
        public async Task<string> DeleteBookingAsync([FromQuery] string bookingId)
        {
            return await bookingService.Value.DeleteBookingAsync(bookingId);
        }
    }
}
