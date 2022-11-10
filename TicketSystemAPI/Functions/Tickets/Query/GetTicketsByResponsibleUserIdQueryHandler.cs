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
using TicketSystemAPI.Entities.Dto;

namespace TaskSystemAPI.Functions.Tickets.Query
{
    public class GetTicketsByResponsibleUserIdQueryHandler : IRequestHandler<GetTicketsByResponsibleUserIdQuery, List<TicketDto>>
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public GetTicketsByResponsibleUserIdQueryHandler(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        Task<List<TicketDto>> IRequestHandler<GetTicketsByResponsibleUserIdQuery, List<TicketDto>>.Handle(GetTicketsByResponsibleUserIdQuery request, CancellationToken cancellationToken)
        {
            var tickets = _context.Tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                TicketName = t.TicketName,
                TicketDescription = t.TicketDescription,
                CategoryTypeId = t.CategoryTypeId,
                StatusId = t.StatusId,
                AddedByUserId = t.AddedByUserId,
                ResponsibleUserId = t.ResponsibleUserId
            }).Where(t => t.ResponsibleUserId == _userContextService.GetUserId).ToList();

            return Task.FromResult(tickets);
        }
    }
}
