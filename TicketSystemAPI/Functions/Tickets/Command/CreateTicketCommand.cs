using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TicketSystemAPI;

namespace TaskSystemAPI.Functions.Tickets.Command
{
    public class CreateTicketCommand : IRequest<Ticket>
    {
        [MaxLength(30)]
        public string TicketName { get; set; }
        public int CategoryTypeId { get; set; }
        public string TicketDescription { get; set; } = string.Empty;
    }
}
