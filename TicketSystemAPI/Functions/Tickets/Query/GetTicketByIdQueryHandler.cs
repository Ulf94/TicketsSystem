using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketSystemAPI;
using TicketSystemAPI.Data;
using TicketSystemAPI.Functions.Tickets.Command;
using TicketSystemAPI.Exceptions;

namespace TaskSystemAPI.Functions.Tickets.Query
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, Ticket>
    {
        private readonly DataContext _context;

        public GetTicketByIdQueryHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Ticket> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.FindAsync(request.Id);

            if (ticket == null)
            {
                throw new NotFoundException("Ticket does not exist.");
            }

            return ticket;
        }
    }
}
