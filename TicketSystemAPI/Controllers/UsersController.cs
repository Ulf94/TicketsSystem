using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketSystemAPI.Data;
using TicketSystemAPI.Entities;
using TicketSystemAPI.Exceptions;
using TicketSystemAPI.Models;
using TicketSystemAPI.Services;

namespace TicketSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAccountService _accountService;

        public UsersController(DataContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
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

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        public async Task<IActionResult> GetCurrentUser()
        {
            if (((ClaimsIdentity)User.Identity).IsAuthenticated == false)
                return Unauthorized();

            var UserId = ((ClaimsIdentity)User.Identity).Claims.ElementAtOrDefault(0).Value;
            var UserName = ((ClaimsIdentity)User.Identity).Claims.ElementAtOrDefault(1).Value;
            var UserRole = ((ClaimsIdentity)User.Identity).Claims.ElementAtOrDefault(2).Value;
            var user = new LoggedUser
            {
                UserName = UserName,
                UserId = UserId,
                UserRole = UserRole
            };

            return Ok(user);

        }

        // POST api/<UserLoginController>
        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] UserLogin dto)
        {
            string tokenGenerated = _accountService.GenerateJwt(dto);
            var user = new UserToken()
            {
                Token = tokenGenerated
            };
            return Ok(user);
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
