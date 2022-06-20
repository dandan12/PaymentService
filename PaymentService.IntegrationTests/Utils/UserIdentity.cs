using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.IntegrationTests.Utils
{
    public class UserIdentity
    {
        public static List<Claim> GetClaims()
        {
            var list = new List<Claim>();
            list.Add(new Claim(ClaimTypes.Name, "Partner 1"));
            list.Add(new Claim(ClaimTypes.NameIdentifier, "c7f8ccd2-6392-4ae1-8445-33ac6306379c"));
            list.Add(new Claim("url-callback", "https://localhost:8000/callback"));

            return list;
        }
    }
}
