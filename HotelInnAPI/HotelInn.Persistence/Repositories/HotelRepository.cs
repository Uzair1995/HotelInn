using HotelInn.Domain.IRepositories;
using HotelInn.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HotelInn.Persistence.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelInnDbContext hotelInnDbContext;

        public HotelRepository(HotelInnDbContext hotelInnDbContext)
        {
            this.hotelInnDbContext = hotelInnDbContext;
        }

        public async Task AddOrUpdateHotelAsync(Hotel hotel)
        {
            hotel.LastUpdateTime = DateTime.Now;
            var alreadySavedData = await hotelInnDbContext.Hotels.FindAsync(hotel.HotelId);

            if (alreadySavedData == null)
            {
                hotel.SaveDateTime = DateTime.Now;
                hotelInnDbContext.Hotels.Add(hotel);
            }
            else
            {
                foreach (PropertyInfo info in hotel.GetType().GetProperties())
                {
                    info.SetValue(alreadySavedData, info.GetValue(hotel));
                }
                hotelInnDbContext.Hotels.Update(alreadySavedData);
            }

            await hotelInnDbContext.SaveChangesAsync();
        }

        public async Task DeleteHotelAsync(string hotelId)
        {
            var alreadySavedData = await hotelInnDbContext.Hotels.FindAsync(hotelId);
            if (alreadySavedData != null)
            {
                hotelInnDbContext.Hotels.Remove(alreadySavedData);
                await hotelInnDbContext.SaveChangesAsync();
            }
        }

        public async Task<Hotel> FindHotelAsync(string hotelId)
        {
            return await hotelInnDbContext.Hotels.FindAsync(hotelId);
        }

        public async Task<List<Hotel>> SearchHotelsAndListAsync(Contracts.Hotel.SearchHotel searchHotel)
        {
            IQueryable<Hotel> query = hotelInnDbContext.Hotels.Where(x => x.Rating >= searchHotel.RatingGreaterThan);

            if (searchHotel.PriceLessThan > 0)
                query = query.Where(x => x.Price <= searchHotel.PriceLessThan);
            if (searchHotel.PriceGreaterThan > 0)
                query = query.Where(x => x.Price >= searchHotel.PriceGreaterThan);

            if (searchHotel.BreakfastFacility)
                query = query.Where(x => x.BreakfastFacility);
            if (searchHotel.WifiFacility)
                query = query.Where(x => x.WifiFacility);
            if (searchHotel.ParkingFacility)
                query = query.Where(x => x.ParkingFacility);
            if (searchHotel.SpaFacility)
                query = query.Where(x => x.SpaFacility);

            if (!string.IsNullOrWhiteSpace(searchHotel.Name))
                query = query.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{searchHotel.Name.ToLower()}%"));

            if (!string.IsNullOrWhiteSpace(searchHotel.City))
                query = query.Where(x => EF.Functions.Like(x.City.ToLower(), searchHotel.City.ToLower()));

            if (searchHotel.Tags != null && searchHotel.Tags.Count > 0)
            {
                foreach (var tag in searchHotel.Tags)
                    query = query.Where(x => EF.Functions.Like(x.Tags.ToLower(), $"%{tag.ToLower()}%"));
            }

            return await query.ToListAsync();
        }
    }
}
