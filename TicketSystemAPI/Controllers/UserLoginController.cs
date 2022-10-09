using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketSystemAPI.Models;
using TicketSystemAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public UserLoginController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        // GET: api/<UserLoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserLoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
        public ActionResult Login([FromBody] UserLogin dto)
        {
            string tokenGenerated = _accountService.GenerateJwt(dto);
            var user = new UserToken ()
            {
                Token = tokenGenerated
            };
            return Ok(user);
        }


        // POST api/<UserLoginController>
        [HttpPost]
        [Route("logout")]
        public async void Logout()
        {
        }


        // DELETE api/<UserLoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
