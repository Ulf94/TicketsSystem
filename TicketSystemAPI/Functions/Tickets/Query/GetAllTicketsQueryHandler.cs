using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TicketSystemAPI;
using TicketSystemAPI.Data;
using TicketSystemAPI.Functions.Tickets.Command;

namespace TaskSystemAPI.Functions.Tickets.Query
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, List<Ticket>>
    {
        private readonly DataContext _context;

        public GetAllTicketsQueryHandler(DataContext context)
        {
            _context = context;
        }

        public Task<List<Ticket>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = _context.Tickets.ToList();

            return Task.FromResult(tickets);
        }
    }
}
