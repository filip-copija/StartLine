using System.Security.Claims;

namespace StartLine_social_network.Extensions
{
    public static class PrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
