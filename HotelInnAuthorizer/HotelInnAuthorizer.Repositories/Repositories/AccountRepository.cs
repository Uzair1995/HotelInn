using HotelInnAuthorizer.Repositories.Interfaces;
using HotelInnAuthorizer.Repositories.Models;
using System.Threading.Tasks;

namespace HotelInnAuthorizer.Repositories.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AuthorizerDbContext authorizerDbContext;

        public AccountRepository(AuthorizerDbContext authorizerDbContext)
        {
            this.authorizerDbContext = authorizerDbContext;
        }

        public async Task<User> GetUserDetailsAsync(string userId)
        {
            var res = await authorizerDbContext.Users.FindAsync(userId);
            return res;
        }
    }
}