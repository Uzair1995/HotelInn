using System.ComponentModel.DataAnnotations;

namespace HotelInn.Contracts.User
{
    public class NewUser
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}