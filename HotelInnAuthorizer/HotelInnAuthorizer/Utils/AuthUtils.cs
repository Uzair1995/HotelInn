using System.Linq;
using System.Security.Claims;

namespace HotelInnAuthorizer.Utils
{
    public static class AuthUtils
    {
        public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Where(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Select(x => x.Value).FirstOrDefault();
        }
    }
}
