using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TicketSystemAPI;
using TicketSystemAPI.Data;
using TicketSystemAPI.Entities.Dto;
using TicketSystemAPI.Functions.Tickets.Command;

namespace TaskSystemAPI.Functions.Tickets.Query
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, List<TicketDto>>
    {
        private readonly DataContext _context;

        public GetAllTicketsQueryHandler(DataContext context)
        {
            _context = context;
        }

        public Task<List<TicketDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = _context.Tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                TicketName = t.TicketName,
                TicketDescription = t.TicketDescription,
                CategoryTypeId = t.CategoryTypeId,
                StatusId = t.StatusId,
                AddedByUserId = t.AddedByUserId,
                ResponsibleUserId = t.ResponsibleUserId,
                CreatedOn = t.CreatedOn,
            }).ToList();

            return Task.FromResult(tickets);
        }
    }
}
