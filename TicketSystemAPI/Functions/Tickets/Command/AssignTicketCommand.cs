using MediatR;
using TicketSystemAPI;

namespace TaskSystemAPI.Functions.Tickets.Command
{
    public class AssignTicketCommand : IRequest<Ticket>
    {
        public int Id { get; set; }
        public int? ResponsibleUserId { get; set; }
    }
}
