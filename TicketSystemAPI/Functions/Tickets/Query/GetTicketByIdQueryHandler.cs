using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketSystemAPI;
using TicketSystemAPI.Data;
using TicketSystemAPI.Functions.Tickets.Command;
using TicketSystemAPI.Exceptions;
using TicketSystemAPI.Entities.Dto;

namespace TaskSystemAPI.Functions.Tickets.Query
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto>
    {
        private readonly DataContext _context;

        public GetTicketByIdQueryHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<TicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.FindAsync(request.Id);

            if (ticket == null)
            {
                throw new NotFoundException("Ticket does not exist.");
            }

            var ticketDto = new TicketDto
            {
                Id = ticket.Id,
                TicketName = ticket.TicketName,
                TicketDescription = ticket.TicketDescription,
                CategoryTypeId = ticket.CategoryTypeId,
                StatusId = ticket.StatusId,
                AddedByUserId = ticket.AddedByUserId,
                ResponsibleUserId = ticket.ResponsibleUserId,
                CreatedOn = ticket.CreatedOn
            };

            return ticketDto;
        }
    }
}
