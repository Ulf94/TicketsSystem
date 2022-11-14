using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TicketSystemAPI.Data;
using TicketSystemAPI.Entities.Dto;

namespace TicketSystemAPI.Functions.Tickets.Query
{
    public class GetAllTicketsUnauthorizedUserQueryHandler : IRequestHandler<GetAllTicketsUnauthorizedUserQuery, List<TicketUnauthorizedDto>>
    {
        private readonly DataContext _dataContext;
        public GetAllTicketsUnauthorizedUserQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Task<List<TicketUnauthorizedDto>> Handle(GetAllTicketsUnauthorizedUserQuery request, CancellationToken cancellationToken)
        {
            List<TicketUnauthorizedDto> ticketsList = _dataContext.Tickets.Select(x => new TicketUnauthorizedDto
            {
                Id = x.Id,
                TicketName = x.TicketName
            }).ToList();

            return Task.FromResult(ticketsList);
        }
    }
}
