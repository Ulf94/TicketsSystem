using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketSystemAPI;

namespace TaskSystemAPI.Functions.Tickets.Command
{
    public class DeleteTicketCommand : IRequest<Unit>
    {
        public int id { get; set; }
    }
}
