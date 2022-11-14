using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TicketSystemAPI;
using TicketSystemAPI.Data;
using TicketSystemAPI.Exceptions;

namespace TaskSystemAPI.Functions.Tickets.Command
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, Ticket>
    {
        private readonly DataContext _context;

        public UpdateTicketCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Ticket> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            if (_context.Tickets.Any(x => x.Id == request.Id))
            {
                var entity = _context.Tickets.FirstOrDefault(x => x.Id == request.Id);
                if (entity != null)
                {
                    entity.TicketName = request.TicketName;
                    entity.CategoryTypeId = request.CategoryTypeId;
                    entity.TicketDescription = request.TicketDescription;
                    entity.StatusId = request.StatusId;
                    switch (entity.StatusId)
                    {
                        case (int)StatusesTypes.Pending:
                            entity.ResponsibleUserId = null;
                            break;
                        case (int)StatusesTypes.In_progress:
                            entity.ResponsibleUserId = request.ResponsibleUserId;
                            break;
                        case (int)StatusesTypes.Done:
                            entity.ResponsibleUserId = null;
                            break;
                    }
                }
                await _context.SaveChangesAsync(cancellationToken);
                return entity;
            }
            else
            {
                throw new BadRequestException("Ticket not found");
            }

        }
    }
}
