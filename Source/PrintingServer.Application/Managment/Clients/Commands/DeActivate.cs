using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Clients.Commands;
public class ClientDeActivateCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
public class ClientDeActivateCommandHandler(ITQLogger<ClientDeActivateCommandHandler> logger,
                                      IClientRepository clientRepository,
                                      IUserContext userContext,
                                      IEntityAuthorizationService<Client> clientAuthorizationService) : IRequestHandler<ClientDeActivateCommand>
{
    public async Task Handle(ClientDeActivateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("De-Activate Client {@Client}", request);
        var client = await clientRepository.GetByIDAsync(request.Id);
        if (client is null) throw new NotFoundException(nameof(Client), request.Id.ToString());
        if (!clientAuthorizationService.Authorize(ResourceOperation.Update, client)) throw new ForbidException();
        try
        {
            client.Deactivate();
            client.AppUserId = userContext.GetCurrentUser()!.Id;
            await clientRepository.Update(client, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Client), client.ToString());
        }
    }
}
