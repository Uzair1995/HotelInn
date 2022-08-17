using HotelInn.Domain.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HotelInn.Domain.Models
{
    public class Hotel : IDatabaseModel
    {
        [Key]
        public string HotelId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public int ReviewsCount { get; set; }
        [Required]
        public double Price { get; set; }
        public bool BreakfastFacility { get; set; }
        public bool WifiFacility { get; set; }
        public bool ParkingFacility { get; set; }
        public bool SpaFacility { get; set; }
        public bool Availability { get; set; }
        public string Tags { get; set; }
        public string Pics { get; set; }

        public Contracts.Hotel.Hotel ToDto()
        {
            return new Contracts.Hotel.Hotel
            {
                HotelId = HotelId,
                Name = Name,
                Address = Address,
                City = City,
                Country = Country,
                Description = Description,
                Rating = Rating,
                ReviewsCount = ReviewsCount,
                Price = Price,
                BreakfastFacility = BreakfastFacility,
                WifiFacility = WifiFacility,
                ParkingFacility = ParkingFacility,
                SpaFacility = SpaFacility,
                Availability = Availability,
                Tags = Tags?.Split(',').ToList(),
                Pics = Pics?.Split(',').ToList()
            };
        }
    }
}
