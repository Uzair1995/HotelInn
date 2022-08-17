using System.Collections.Generic;

namespace HotelInn.Contracts.Hotel
{
    public class SearchHotel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public double RatingGreaterThan { get; set; }
        public double PriceLessThan { get; set; }
        public double PriceGreaterThan { get; set; }
        public bool BreakfastFacility { get; set; }
        public bool WifiFacility { get; set; }
        public bool ParkingFacility { get; set; }
        public bool SpaFacility { get; set; }
        public List<string> Tags { get; set; }
    }
}
