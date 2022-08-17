using HotelInn.Contracts.Booking;
using HotelInn.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
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
    }
}
