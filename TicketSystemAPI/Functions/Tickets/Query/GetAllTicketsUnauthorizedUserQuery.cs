using MediatR;
using System.Collections.Generic;
using TicketSystemAPI.Entities.Dto;

namespace TicketSystemAPI.Functions.Tickets.Query
{
    public class GetAllTicketsUnauthorizedUserQuery : IRequest<List<TicketUnauthorizedDto>>
    {
    }
}
