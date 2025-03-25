using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Clients.Commands;
public class ClientActivateCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
public class ClientActivateCommandHandler(ITQLogger<ClientActivateCommandHandler> logger,
                                          IClientRepository clientRepository,
                                          IUserContext userContext,
                                          IEntityAuthorizationService<Client> clientAuthorizationService) : IRequestHandler<ClientActivateCommand>
{
    public async Task Handle(ClientActivateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Activate Client {@Client}", request);
        var client = await clientRepository.GetByIDAsync(request.Id);
        if (client is null) throw new NotFoundException(nameof(Client), request.Id.ToString());
        if (!clientAuthorizationService.Authorize(ResourceOperation.Update, client)) throw new ForbidException();
        try
        {
            client.Activate();
            client.AppUserId = userContext.GetCurrentUser()!.Id;
            await clientRepository.Update(client, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Client), client.ToString());
        }
    }
}
