using HotelInnAuthorizer.Repositories.Models;
using System;

namespace HotelInnAuthorizer.Services.Models
{
    public class RegisterAccount
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }

        public User ToCore()
        {
            return new User
            {
                UserName = Name,
                Id = Name,
                Name = Name,
                Role = Role,
                Gender = Gender,
                Address = Address,
                City = City,
                Country = Country
            };
        }
    }
}