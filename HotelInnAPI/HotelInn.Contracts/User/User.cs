using System.ComponentModel.DataAnnotations;

namespace HotelInn.Contracts.User
{
    public class User : NewUser
    {
        [Required]
        public string UserId { get; set; }
    }
}
