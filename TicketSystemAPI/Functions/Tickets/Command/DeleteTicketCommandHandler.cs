using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TicketSystemAPI.Authorization;
using TicketSystemAPI.Data;
using TicketSystemAPI.Exceptions;
using TicketSystemAPI.Services;

namespace TaskSystemAPI.Functions.Tickets.Command
{
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand>
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;

        public DeleteTicketCommandHandler(DataContext context, 
                                            IUserContextService userContextService,
                                            IAuthorizationService authorizationService)
        {
            _context = context;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }

        public async Task<Unit> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Tickets.FirstOrDefault(x => x.Id == request.id);
            if (entity == null)
            {
                throw new NotFoundException("Ticket not found");
            }
            try
            {
                var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, entity, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
                if (authorizationResult.Succeeded) // ToDo
                {
                    _context.Tickets.Remove(entity);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                throw new BadRequestException("Ticket was not removed.");
            }
            catch
            {
                throw new BadRequestException("Ticket was not removed");
            }
            return Unit.Value;
        }
    }
}
