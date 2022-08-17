﻿using HotelInn.Domain.IRepositories;
using HotelInn.Services.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelInn.Services.Services
{
    public class HotelService : IHotelService
    {
        private readonly Lazy<IHotelRepository> hotelRepository;

        public HotelService(Lazy<IHotelRepository> hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }

        public async Task<string> AddNewHotelAsync(Contracts.Hotel.NewHotel hotel)
        {
            if (hotel == null)
                return "Value cannot be null";

            Domain.Models.Hotel newHotel = new Domain.Models.Hotel
            {
                Name = hotel.Name,
                Address = hotel.Address,
                City = hotel.City,
                Country = hotel.Country,
                Description = hotel.Description,
                Rating = hotel.Rating,
                ReviewsCount = hotel.ReviewsCount,
                Price = hotel.Price,
                BreakfastFacility = hotel.BreakfastFacility,
                WifiFacility = hotel.WifiFacility,
                ParkingFacility = hotel.ParkingFacility,
                SpaFacility = hotel.SpaFacility,
                Availability = hotel.Availability,
                Tags = hotel.Tags != null && hotel.Tags.Count > 0 ? string.Join(',', hotel.Tags) : null,
                Pics = hotel.Pics != null && hotel.Pics.Count > 0 ? string.Join(',', hotel.Pics) : null,
            };

            await hotelRepository.Value.AddOrUpdateHotelAsync(newHotel);
            return "Saved successfully!";
        }

        public async Task<string> UpdateHotelAsync(Contracts.Hotel.Hotel hotel)
        {
            if (hotel == null || hotel.HotelId == null)
                return "Value cannot be null";

            var alreadySavedData = await hotelRepository.Value.FindHotelAsync(hotel.HotelId);
            if (alreadySavedData == null)
                return "Hotel ID does not match any records!";

            Domain.Models.Hotel updateHotel = new Domain.Models.Hotel
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Address = hotel.Address,
                City = hotel.City,
                Country = hotel.Country,
                Description = hotel.Description,
                Rating = hotel.Rating,
                ReviewsCount = hotel.ReviewsCount,
                Price = hotel.Price,
                BreakfastFacility = hotel.BreakfastFacility,
                WifiFacility = hotel.WifiFacility,
                ParkingFacility = hotel.ParkingFacility,
                SpaFacility = hotel.SpaFacility,
                Availability = hotel.Availability,
                Tags = hotel.Tags != null && hotel.Tags.Count > 0 ? string.Join(',', hotel.Tags) : null,
                Pics = hotel.Pics != null && hotel.Pics.Count > 0 ? string.Join(',', hotel.Pics) : null
            };

            await hotelRepository.Value.AddOrUpdateHotelAsync(updateHotel);
            return "Update successfully!";
        }

        public async Task<Contracts.Hotel.Hotel> FindHotelAsync(string hotelId)
        {
            if (string.IsNullOrWhiteSpace(hotelId))
                return null;

            var hotel = await hotelRepository.Value.FindHotelAsync(hotelId);
            if (hotel == null)
                return null;

            return new Contracts.Hotel.Hotel
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Address = hotel.Address,
                City = hotel.City,
                Country = hotel.Country,
                Description = hotel.Description,
                Rating = hotel.Rating,
                ReviewsCount = hotel.ReviewsCount,
                Price = hotel.Price,
                BreakfastFacility = hotel.BreakfastFacility,
                WifiFacility = hotel.WifiFacility,
                ParkingFacility = hotel.ParkingFacility,
                SpaFacility = hotel.SpaFacility,
                Availability = hotel.Availability,
                Tags = hotel.Tags?.Split(',').ToList(),
                Pics = hotel.Pics?.Split(',').ToList()
            };
        }

        public async Task<string> DeleteHotelAsync(string hotelId)
        {
            if (string.IsNullOrWhiteSpace(hotelId))
                return "Hotel ID is not provided!";

            var hotel = await hotelRepository.Value.FindHotelAsync(hotelId);
            if (hotel == null)
                return "Hotel ID does not match any records!";

            await hotelRepository.Value.DeleteHotelAsync(hotelId);
            return "Delete successfully!";
        }
    }
}