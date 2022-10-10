using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TicketSystemAPI;
using TicketSystemAPI.Data;
using TicketSystemAPI.Exceptions;

namespace TaskSystemAPI.Functions.Tickets.Command
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Ticket>
    {
        private readonly DataContext _context;

        public CreateTicketCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Ticket> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            Ticket createdTicket = new Ticket
            {
                TicketName = request.TicketName,
                AddedByUserId = request.AddedByUserId,
                CategoryTypeId = request.CategoryTypeId,
                ResponsibleUserId = request.ResponsibleUserId,
                TicketDescription = request.TicketDescription,
                StatusId = 1
                
            };

            _context.Tickets.Add(createdTicket);

            try
            {
                await _context.SaveChangesAsync();
                return createdTicket;
            }
            catch
            {
                throw new BadRequestException("Ticket was not added");
            }

        }
    }
}
