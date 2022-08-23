using System.Collections.Generic;

namespace HotelInnAuthorizer.Services.Models
{
    public class RegisterResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}