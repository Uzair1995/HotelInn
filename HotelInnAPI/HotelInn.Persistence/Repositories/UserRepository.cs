using HotelInn.Domain.IRepositories;
using HotelInn.Domain.Models;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace HotelInn.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HotelInnDbContext hotelInnDbContext;

        public UserRepository(HotelInnDbContext hotelInnDbContext)
        {
            this.hotelInnDbContext = hotelInnDbContext;
        }

        public async Task AddOrUpdateUserAsync(User user)
        {
            user.LastUpdateTime = DateTime.Now;
            var alreadySavedData = await hotelInnDbContext.Users.FindAsync(user.UserId);

            if (alreadySavedData == null)
            {
                user.SaveDateTime = DateTime.Now;
                hotelInnDbContext.Users.Add(user);
            }
            else
            {
                foreach (PropertyInfo info in user.GetType().GetProperties())
                {
                    info.SetValue(alreadySavedData, info.GetValue(user));
                }
                hotelInnDbContext.Users.Update(alreadySavedData);
            }

            await hotelInnDbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var alreadySavedData = await hotelInnDbContext.Users.FindAsync(userId);
            if (alreadySavedData != null)
            {
                hotelInnDbContext.Users.Remove(alreadySavedData);
                await hotelInnDbContext.SaveChangesAsync();
            }
        }

        public async Task<User> FindUserAsync(string userId)
        {
            return await hotelInnDbContext.Users.FindAsync(userId);
        }
    }
}
