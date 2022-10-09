using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketSystemAPI.Models;
using TicketSystemAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisterController : ControllerBase
    {

        private readonly IAccountService _accountService;

        public UserRegisterController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // POST api/<UserRegisterController>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegister)
        {
            _accountService.RegisterUser(userRegister);
            return Ok();
        }
    }
}
