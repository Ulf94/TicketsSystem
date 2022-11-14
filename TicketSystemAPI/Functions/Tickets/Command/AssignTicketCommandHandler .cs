using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TicketSystemAPI;
using TicketSystemAPI.Data;
using TicketSystemAPI.Exceptions;
using TicketSystemAPI.Services;

namespace TaskSystemAPI.Functions.Tickets.Command
{
    public class AssignTicketCommandHandler : IRequestHandler<AssignTicketCommand, Ticket>
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public AssignTicketCommandHandler(DataContext context,
                                            IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;   
        }

        public async Task<Ticket> Handle(AssignTicketCommand request, CancellationToken cancellationToken)
        {
            if (_context.Tickets.Any(x => x.Id == request.Id))
            {
                var entity = _context.Tickets.FirstOrDefault(x => x.Id == request.Id);
                if (entity != null)
                {
                    entity.ResponsibleUserId = (int)_userContextService.GetUserId;
                    entity.StatusId = (int)StatusesTypes.In_progress;
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
