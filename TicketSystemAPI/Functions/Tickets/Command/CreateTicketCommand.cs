using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketSystemAPI;

namespace TaskSystemAPI.Functions.Tickets.Command
{
    public class CreateTicketCommand : IRequest<Ticket>
    {
        public string TicketName { get; set; }
        public int CategoryTypeId { get; set; }
        public string TicketDescription { get; set; } = string.Empty;
        public string Status { get; set; }
        public int AddedByUserId { get; set; }
        public int? ResponsibleUserId { get; set; } = null!;
    }
}
