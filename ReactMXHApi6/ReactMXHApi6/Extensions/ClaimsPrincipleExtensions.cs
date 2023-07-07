using System.Security.Claims;

namespace ReactMXHApi6.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        // Obtém o nome de usuário a partir do objeto ClaimsPrincipal
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name).Value;
        }

        // Obtém o ID do usuário a partir do objeto ClaimsPrincipal
        public static string GetUserId(this ClaimsPrincipal user)
        {
            var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return id;
        }
    }
}
