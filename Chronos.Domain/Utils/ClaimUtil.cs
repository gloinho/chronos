using Microsoft.AspNetCore.Http;

namespace Chronos.Domain.Utils
{
    public static class ClaimUtil
    {
        public static string GetClaim(this HttpContext httpContext, string claimTypes)
        {
            var claim = httpContext?.User?.Claims;

            if (claim != null && claim.Any())
            {
                try
                {
                    var value = claim.FirstOrDefault(x => x.Type.Equals(claimTypes)).Value;
                    return value;
                }
                catch (Exception e)
                {
                    return string.Empty;
                }
            }

            return null;
        }
    }
}
