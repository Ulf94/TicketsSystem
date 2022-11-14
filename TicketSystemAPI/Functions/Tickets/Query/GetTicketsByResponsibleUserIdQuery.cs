using MediatR;
using System.Collections.Generic;
using TicketSystemAPI;
using TicketSystemAPI.Entities.Dto;

namespace TaskSystemAPI.Functions.Tickets.Query
{
    public class GetTicketsByResponsibleUserIdQuery : IRequest<List<TicketDto>>
    {
        public int ResponsibleUserId { get; set; }
    }
}
