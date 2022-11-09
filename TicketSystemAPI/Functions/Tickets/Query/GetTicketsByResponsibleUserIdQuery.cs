using MediatR;
using System.Collections.Generic;
using TicketSystemAPI;

namespace TaskSystemAPI.Functions.Tickets.Query
{
    public class GetTicketsByResponsibleUserIdQuery : IRequest<List<Ticket>>
    {
        public int ResponsibleUserId { get; set; }
    }
}
