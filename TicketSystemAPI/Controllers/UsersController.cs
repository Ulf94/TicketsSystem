using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketSystemAPI.Data;
using TicketSystemAPI.Entities;
using TicketSystemAPI.Entities.Dto;
using TicketSystemAPI.Exceptions;
using TicketSystemAPI.Services;

namespace TicketSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService; 
        private readonly IPasswordHasher<User> _passwordHasher;


        public UsersController(DataContext context, IAccountService accountService, ITokenService tokenService, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _accountService = accountService;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;

        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _context.Users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                RoleId = u.RoleId,
            }).ToListAsync();

            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                var existingUser = _context.Users.Where(x => x.Id == id).FirstOrDefault();
                if (existingUser != null)
                {
                    user.PasswordHash = existingUser.PasswordHash;
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPatch]
        [AllowAnonymous]
        public async Task<IActionResult> PatchUser([FromBody] UserPatchRequestDto userPatchRequest)
        {
            var user = _context.Users.Where(x => x.Id == userPatchRequest.Id).FirstOrDefault();
            
            if(user == null) 
                return NotFound();
            else
            {
                _context.Entry(user).State = EntityState.Modified;

                try
                {
                    user.FirstName = userPatchRequest.FirstName;
                    user.LastName = userPatchRequest.LastName;
                    user.Email = userPatchRequest.Email;
                    user.Nationality = userPatchRequest.Nationality;
                    user.RoleId = userPatchRequest.RoleId;
                    user.DateOfBirth = userPatchRequest.DateOfBirth;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userPatchRequest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegister)
        {
            _accountService.RegisterUser(userRegister);
            return Ok();
        }

        [HttpGet]
        [Route("getCurrentUser")]
        [Authorize(Roles = "Manager, Admin, User ")]
        public async Task<IActionResult> GetCurrentUser()
        {
            if (((ClaimsIdentity)User.Identity).IsAuthenticated == false)
                return Unauthorized();

            var UserId = ((ClaimsIdentity)User.Identity).Claims.ElementAtOrDefault(0).Value;
            var UserName = ((ClaimsIdentity)User.Identity).Claims.ElementAtOrDefault(1).Value;
            var UserRole = ((ClaimsIdentity)User.Identity).Claims.ElementAtOrDefault(2).Value;

            var user = new LoggedUserDto
            {
                UserId = UserId,
                UserName = UserName,
                UserRole = UserRole
            };

            return Ok(user);

        }

        // POST api/<UserLoginController>
        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] UserLoginDto dto)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}")

            };

            if (!string.IsNullOrEmpty(user.Nationality))
            {
                claims.Add(
                    new Claim("Nationality", user.Nationality)
                    );
            }
            string tokenGenerated = _tokenService.GenerateJwt(claims);
            string refreshTokenGenerated = _tokenService.GenerateRefreshToken();
            var tokens = new UserTokenDto()
            {
                Token = tokenGenerated,
                RefreshToken = refreshTokenGenerated                
            };

            user.RefreshToken = refreshTokenGenerated;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);

            _context.SaveChanges();

            return Ok(tokens);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if(user.Email == "admin")
            {
                throw new ForbidException("Cannot remove admin"); 
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
