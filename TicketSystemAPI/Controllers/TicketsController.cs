using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskSystemAPI.Functions.Tickets.Command;
using TaskSystemAPI.Functions.Tickets.Query;
using TicketSystemAPI;
using TicketSystemAPI.Data;
using TicketSystemAPI.Entities.Dto;
using TicketSystemAPI.Functions.Tickets.Command;
using TicketSystemAPI.Functions.Tickets.Query;
using TicketSystemAPI.Services;

namespace TicketSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public TicketsController(IMediator mediator, DataContext dataContext, IUserContextService userContextService)
        {
            _mediator = mediator;
            _context = dataContext;
            _userContextService = userContextService;
        }


        // GET: api/Tickets
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetTickets()
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = await _mediator.Send(new GetAllTicketsQuery());
            return Ok(result);
        }

        [Route("byResponsibleUser")]
        [HttpGet]
        public async Task<ActionResult> GetTicketsByResponsibleUser()
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = await _mediator.Send(new GetTicketsByResponsibleUserIdQuery());
            return Ok(result);
        }



        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetTicket(int id)
        {
            var request = new GetTicketByIdQuery { Id = id };
            var ticket = await _mediator.Send(request);
            return Ok(ticket);
        }

       
        [HttpPatch]
        public async Task<IActionResult> PatchTicket(UpdateTicketCommand ticket)
        {
            await _mediator.Send(ticket);
            return Ok();
        }

        [Route("assignTicket")]
        [HttpPatch]
        [Authorize(Roles = "Manager, Admin, User ")]
        public async Task<IActionResult> AssignTicket(AssignTicketCommand ticket)
        {
            var result = await _mediator.Send(ticket);
            return Ok(result);
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Manager, User")]
        public async Task<ActionResult<Ticket>> PostTicket(CreateTicketCommand ticket)
        {
            var result = await _mediator.Send(ticket);
            return Ok(result);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var request = new DeleteTicketCommand { id = id };
            await _mediator.Send(request);
            return Ok();
        }

        
    }
}
