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
using TicketSystemAPI.Functions.Tickets.Command;

namespace TicketSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DataContext _context;

        public TicketsController(IMediator mediator, DataContext dataContext)
        {
            _mediator = mediator;
            _context = dataContext;
        }


        // GET: api/Tickets
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _mediator.Send(new GetAllTicketsQuery());
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var request = new GetTicketByIdQuery { Id = id };
            var ticket = await _mediator.Send(request);
            return ticket;
        }

       
        [HttpPatch]
        //[Authorize(Roles ="Manager, Admin ")]
        public async Task<IActionResult> PatchTicket(UpdateTicketCommand ticket)
        {
            await _mediator.Send(ticket);
            return Ok();
        }

       
        [HttpPost]
        //[Authorize(Roles = "Admin, Manager, User")]
        public async Task<ActionResult<Ticket>> PostTicket(CreateTicketCommand ticket)
        {
            return await _mediator.Send(ticket);
        }

        // DELETE: api/Tickets/5
        [HttpDelete]
        //[Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteTicket(DeleteTicketCommand ticket)
        {
            await _mediator.Send(ticket);
            return Ok();
        }

        
    }
}
