using HotelInn.Contracts.Booking;
using HotelInn.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelInn.Presentation.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    [SwaggerTag("APIs to manage hotels.")]

    public class BookingController
    {
        private readonly Lazy<IBookingService> bookingService;

        public BookingController(Lazy<IBookingService> bookingService)
        {
            this.bookingService = bookingService;
        }

        [SwaggerOperation(Summary = "Place a booking.")]
        [HttpPost]
        public async Task<string> AddNewBookingAsync(NewBooking newBooking)
        {
            return await bookingService.Value.AddNewBookingAsync(newBooking);
        }

        [SwaggerOperation(Summary = "Get a specific booking details.")]
        [HttpGet]
        public async Task<Booking> GetBookingDetailsAsync([FromQuery] string bookingId)
        {
            return await bookingService.Value.FindBookingAsync(bookingId);
        }

        [SwaggerOperation(Summary = "List all bookings of a specific user.")]
        [HttpGet("userbookings")]
        public async Task<List<Booking>> GetUserBookingsListAsync([FromQuery] string userId)
        {
            return await bookingService.Value.FindUserBookingsAsync(userId);
        }

        [SwaggerOperation(Summary = "List all bookings of a specific hotel.")]
        [HttpGet("hotelbookings")]
        public async Task<List<Booking>> GetHotelBookingsListAsync([FromQuery] string hotelId)
        {
            return await bookingService.Value.FindHotelBookingsAsync(hotelId);
        }
    }
}
