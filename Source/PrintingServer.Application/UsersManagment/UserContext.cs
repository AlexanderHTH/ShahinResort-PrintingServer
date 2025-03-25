using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PrintingServer.Domain.Exceptions;

namespace PrintingServer.Application.UsersManagment
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }

    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = (httpContextAccessor?.HttpContext?.User) ?? throw new NotFoundException(nameof(UserContext), "User context is not present.");
            if (user.Identity == null || !user.Identity.IsAuthenticated)
                return null;
            var userId = Guid.Parse(user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            var userEmail = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var userRoles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
            var userName = user.Claims.Where(c => c.Type == ClaimTypes.Name)!.First().Value;
            return new CurrentUser(userId, userEmail, userName, userRoles);
        }
    }
}
