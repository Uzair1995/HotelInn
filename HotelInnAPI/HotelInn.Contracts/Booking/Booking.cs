using System.ComponentModel.DataAnnotations;

namespace HotelInn.Contracts.Booking
{
    public class Booking : NewBooking
    {
        [Required]
        public string BookingId { get; set; }
    }
}
