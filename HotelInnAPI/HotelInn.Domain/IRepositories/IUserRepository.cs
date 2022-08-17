using HotelInn.Domain.Models;
using System.Threading.Tasks;

namespace HotelInn.Domain.IRepositories
{
    public interface IUserRepository
    {
        Task AddOrUpdateUserAsync(User user);
        Task<User> FindUserAsync(string userId);
        Task DeleteUserAsync(string userId);
    }
}
