using MediatR;
using System.Collections.Generic;
using TicketSystemAPI;
using TicketSystemAPI.Entities.Dto;

namespace TicketSystemAPI.Functions.Tickets.Command
{
    public class GetAllTicketsQuery : IRequest<List<TicketDto>>
    {
    }
}
