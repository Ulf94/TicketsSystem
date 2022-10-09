using MediatR;
using TicketSystemAPI;

namespace TaskSystemAPI.Functions.Tickets.Query
{
    public class GetTicketByIdQuery : IRequest<Ticket>
    {
        public int Id { get; set; }
    }
}
