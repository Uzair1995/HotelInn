using System;

namespace HotelInn.Contracts.Booking
{
    public class NewBooking
    {
        public string UserId { get; set; }
        public string HotelId { get; set; }
        public DateTime CheckinDateTime { get; set; }
        public DateTime CheckoutDateTime { get; set; }
    }
}