using Microsoft.AspNetCore.Identity;

namespace HotelInnAuthorizer.Repositories.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}