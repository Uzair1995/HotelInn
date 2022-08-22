using HotelInnAuthorizer.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelInnAuthorizer.Models
{
    public class RegisterAccount
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }

        public Services.Models.RegisterAccount ToDto(string role)
        {
            return new Services.Models.RegisterAccount
            {
                Name = Name,
                Password = Password,
                Role = role,
                Gender = Gender,
                Address = Address,
                City = City,
                Country = Country
            };
        }
    }
}
