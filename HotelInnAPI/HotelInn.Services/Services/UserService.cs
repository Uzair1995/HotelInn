using HotelInn.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelInn.Services.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {

        }

        public async Task<Domain.Models.User> FindUserAsync(string userId)
        {

        }
    }
}
