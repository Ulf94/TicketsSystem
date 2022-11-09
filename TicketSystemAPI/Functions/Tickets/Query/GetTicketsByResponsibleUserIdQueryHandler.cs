using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TicketSystemAPI;
using TicketSystemAPI.Data;
using TicketSystemAPI.Functions.Tickets.Command;
using TicketSystemAPI.Exceptions;
using System.Collections.Generic;
using System.Linq;
using TicketSystemAPI.Services;

namespace TaskSystemAPI.Functions.Tickets.Query
{
    public class GetTicketsByResponsibleUserIdQueryHandler : IRequestHandler<GetTicketsByResponsibleUserIdQuery, List<Ticket>>
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public GetTicketsByResponsibleUserIdQueryHandler(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        Task<List<Ticket>> IRequestHandler<GetTicketsByResponsibleUserIdQuery, List<Ticket>>.Handle(GetTicketsByResponsibleUserIdQuery request, CancellationToken cancellationToken)
        {
            var tickets = _context.Tickets.Where(t => t.ResponsibleUserId == _userContextService.GetUserId).ToList();

            return Task.FromResult(tickets);
        }
    }
}
