using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelInn.Services.Abstractions
{
    public interface IUserService
    {
        Task<string> AddNewUserAsync(Contracts.User.NewUser newUser);
        Task<Contracts.User.User> FindUserAsync(string userId);
        Task<List<Contracts.User.User>> ListAllUsersAsync();
    }
}
