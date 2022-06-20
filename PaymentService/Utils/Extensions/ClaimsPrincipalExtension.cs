using System.Security.Claims;

namespace PaymentService.Utils.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetPartnerId(this ClaimsPrincipal principal)
        {
            var identity = (ClaimsIdentity)principal.Identity;
            var claim = identity.Claims.Where(x => x.Type.Equals(ClaimTypes.NameIdentifier)).FirstOrDefault();
            return claim?.Value;
        }
    }
}
