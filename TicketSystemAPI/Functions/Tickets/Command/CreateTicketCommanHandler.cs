using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TicketSystemAPI;
using TicketSystemAPI.Data;
using TicketSystemAPI.Exceptions;
using TicketSystemAPI.Services;

namespace TaskSystemAPI.Functions.Tickets.Command
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Ticket>
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public CreateTicketCommandHandler(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<Ticket> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            Ticket createdTicket = new Ticket
            {
                TicketName = request.TicketName,
                CategoryTypeId = request.CategoryTypeId,
                TicketDescription = request.TicketDescription,
                AddedByUserId = (int)_userContextService.GetUserId,
                CreatedOn = System.DateTime.Now
            };
            _context.Tickets.Add(createdTicket);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return createdTicket;
            }
            catch
            {
                throw new BadRequestException("Ticket was not added");
            }

        }
    }
}
