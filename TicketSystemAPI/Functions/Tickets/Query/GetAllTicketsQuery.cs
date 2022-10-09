using MediatR;
using System.Collections.Generic;
using TicketSystemAPI;

namespace TicketSystemAPI.Functions.Tickets.Command
{
    public class GetAllTicketsQuery : IRequest<List<Ticket>>
    {
    }
}
