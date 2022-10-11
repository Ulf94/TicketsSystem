using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TicketSystemAPI;
using TicketSystemAPI.Data;
using TicketSystemAPI.Exceptions;

namespace TaskSystemAPI.Functions.Tickets.Command
{
    public class AssignTicketCommandHandler : IRequestHandler<AssignTicketCommand, Ticket>
    {
        private readonly DataContext _context;

        public AssignTicketCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Ticket> Handle(AssignTicketCommand request, CancellationToken cancellationToken)
        {
            if (_context.Tickets.Any(x => x.Id == request.Id))
            {
                var entity = _context.Tickets.FirstOrDefault(x => x.Id == request.Id);
                if (entity != null)
                {
                    entity.ResponsibleUserId = request.ResponsibleUserId;
                }
                await _context.SaveChangesAsync(cancellationToken);
                return entity;
            }
            else
            {
                throw new BadRequestException("Ticket was not assigned");
            }

        }
    }
}
