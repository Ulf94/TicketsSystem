using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketSystemAPI;

namespace TaskSystemAPI.Functions.Tickets.Command
{
    public record DeleteTicketCommand(int id) : IRequest<Unit>;
}
