using System.ComponentModel.DataAnnotations;

namespace HotelInnAuthorizer.Models
{
    public class LoginUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
