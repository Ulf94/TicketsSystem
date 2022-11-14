using MediatR;
using TicketSystemAPI;
using TicketSystemAPI.Entities.Dto;

namespace TaskSystemAPI.Functions.Tickets.Query
{
    public class GetTicketByIdQuery : IRequest<TicketDto>
    {
        public int Id { get; set; }
    }
}
