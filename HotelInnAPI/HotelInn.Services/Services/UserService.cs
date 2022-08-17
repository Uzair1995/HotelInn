using HotelInn.Domain.IRepositories;
using HotelInn.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace HotelInn.Services.Services
{
    public class UserService : IUserService
    {
        private readonly Lazy<IUserRepository> userRepository;

        public UserService(Lazy<IUserRepository> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<string> AddNewUserAsync(Contracts.User.NewUser newUser)
        {
            if (newUser == null)
                return "Value cannot be null";

            Domain.Models.User user = new Domain.Models.User
            {
                Name = newUser.Name,
                Address = newUser.Address,
                City = newUser.City,
                Country = newUser.Country
            };

            await userRepository.Value.AddOrUpdateUserAsync(user);
            return "Saved successfully!";
        }

        public async Task<Contracts.User.User> FindUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return null;

            Domain.Models.User user = await userRepository.Value.FindUserAsync(userId);
            if (user == null)
                return null;

            return user.ToDto();
        }

        public async Task<List<Contracts.User.User>> ListAllUsersAsync()
        {
            List<Domain.Models.User> users = await userRepository.Value.ListAllUsersAsync();
            return users.Select(x => x.ToDto()).ToList();
        }
    }
}
