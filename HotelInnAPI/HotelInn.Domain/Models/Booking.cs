using HotelInn.Domain.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelInn.Domain.Models
{
    public class Booking : IDatabaseModel
    {
        [Key]
        public string BookingId { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        [ForeignKey(nameof(Hotel))]
        public string HotelId { get; set; }
        public DateTime CheckinDateTime { get; set; }
        public DateTime CheckoutDateTime { get; set; }

        public Contracts.Booking.Booking ToDto()
        {
            return new Contracts.Booking.Booking
            {
                BookingId = BookingId,
                Username = UserId,
                HotelId = HotelId,
                CheckinDateTime = CheckinDateTime,
                CheckoutDateTime = CheckoutDateTime
            };
        }
    }
}
