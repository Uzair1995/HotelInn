using HotelInn.Domain.Models;
using System.Threading.Tasks;

namespace HotelInn.Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<User> FindUserAsync(string httpAccessToken);
    }
}
