using System.ComponentModel.DataAnnotations;

namespace HotelInn.Contracts.Hotel
{
    public class Hotel : NewHotel
    {
        [Required]
        public string HotelId { get; set; }
    }
}
