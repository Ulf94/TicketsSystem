using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TicketSystemAPI;
using TicketSystemAPI.Data;
using TicketSystemAPI.Exceptions;

namespace TaskSystemAPI.Functions.Tickets.Command
{
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand>
    {
        private readonly DataContext _context;

        public DeleteTicketCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Tickets.FirstOrDefault(x => x.Id == request.id);
            if (entity == null)
            {
                throw new NotFoundException("Ticket not found");
            }
            try
            {
                _context.Tickets.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                throw new BadRequestException("Ticket was not removed");
            }
            return Unit.Value;
        }
    }
}
