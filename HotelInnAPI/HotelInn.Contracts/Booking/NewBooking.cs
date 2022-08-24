using System;
using System.ComponentModel.DataAnnotations;

namespace HotelInn.Contracts.Booking
{
    public class NewBooking
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string HotelId { get; set; }
        public DateTime CheckinDateTime { get; set; }
        public DateTime CheckoutDateTime { get; set; }
    }
}