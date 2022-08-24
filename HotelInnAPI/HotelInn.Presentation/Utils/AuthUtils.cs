using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace HotelInn.Presentation.Utils
{
    public static class AuthUtils
    {
        public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Where(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Select(x => x.Value).FirstOrDefault();
        }

        public static string GetAccessToken(this HttpContext httpContext)
        {
            return httpContext.Request.Headers.Where(x => x.Key == "Authorization").Select(x => x.Value).FirstOrDefault();
        }
    }
}
