using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelInn.Contracts.Hotel
{
    public class NewHotel
    {
        [Required] public string Name { get; set; }
        [Required] public string Address { get; set; }
        [Required] public string City { get; set; }
        [Required] public string Country { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public int ReviewsCount { get; set; }
        [Required] public double Price { get; set; }
        public bool BreakfastFacility { get; set; }
        public bool WifiFacility { get; set; }
        public bool ParkingFacility { get; set; }
        public bool SpaFacility { get; set; }
        public bool Availability { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Pics { get; set; }
    }
}
