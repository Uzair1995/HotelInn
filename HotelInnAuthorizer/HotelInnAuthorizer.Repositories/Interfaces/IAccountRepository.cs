using HotelInnAuthorizer.Repositories.Models;
using System.Threading.Tasks;

namespace HotelInnAuthorizer.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<User> GetUserDetailsAsync(string userId);
    }
}
