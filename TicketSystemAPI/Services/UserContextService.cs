using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TicketSystemAPI.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
        int? GetUserRoleId { get; } 
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

        public int? GetUserId =>
           User.Identity.Name is null ? 0 : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
        

        public int? GetUserRoleId =>
            User.Identity.Name is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.Role).Value);


    }
}
