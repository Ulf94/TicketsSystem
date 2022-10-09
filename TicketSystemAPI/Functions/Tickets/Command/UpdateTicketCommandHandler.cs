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
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, Ticket>
    {
        private readonly DataContext _context;

        public UpdateTicketCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Ticket> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Tickets.FirstOrDefault(x => x.Id == request.Id);
            if(entity == null)
            {
                throw new BadRequestException("Ticket not found");
            }
            _context.Tickets.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
