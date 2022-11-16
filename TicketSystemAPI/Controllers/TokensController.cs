using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using TicketSystemAPI.Data;
using TicketSystemAPI.Entities.Dto;
using TicketSystemAPI.Services;

namespace TicketSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ITokenService _tokenService;
        //private readonly IUserContextService _userContextService;
        public TokensController(DataContext dataContext, ITokenService tokenService)
        {
            this._dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this._tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(UserTokenDto dto)
        {
            if (dto is null)
                return BadRequest("Invalid client request");

            string accessToken = dto.Token;
            string refreshToken = dto.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var userId = (int?)int.Parse(principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var user = _dataContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = _tokenService.GenerateJwt(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            _dataContext.SaveChanges();
            return Ok(new UserTokenDto()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
        [HttpPost, Authorize]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var userId = (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var user = _dataContext.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null)
                return BadRequest();
            user.RefreshToken = null;
            _dataContext.SaveChanges();
            return NoContent();
        }
    }
}
