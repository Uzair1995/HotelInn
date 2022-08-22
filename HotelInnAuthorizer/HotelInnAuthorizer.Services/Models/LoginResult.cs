namespace HotelInnAuthorizer.Services.Models
{
    public class LoginResult
    {
        public string Token { get; set; }
        public string Result { get; set; }
        public bool Succeeded { get; set; }
    }
}
