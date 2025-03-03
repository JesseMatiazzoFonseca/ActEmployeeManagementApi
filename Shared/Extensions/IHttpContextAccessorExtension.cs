using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Shared.Extensions
{
    public static class IHttpContextAccessorExtension
    {
        public static string CurrentUser(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor?.HttpContext?.User?.FindFirst("Id")?.Value;
        }

        public static string CurrentRoles(this IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor?.HttpContext?.User;

            if (user == null)
                return string.Empty;

            var roles = user.Claims
                            .Where(c => c.Type == ClaimTypes.Role || c.Type == "role")
                            .Select(c => c.Value)
                            .ToList();

            return roles.Any() ? string.Join(",", roles) : "Sem Role";
        }

        public static string GetAuthorizationHeader(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor?.HttpContext?.Request.Headers["Authorization"];
        }
    }
}
