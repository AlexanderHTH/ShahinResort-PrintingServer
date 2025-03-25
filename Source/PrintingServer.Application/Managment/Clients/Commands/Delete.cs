using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Clients.Commands;

public class ClientDeleteCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
public class ClientDeleteCommandHandler(ITQLogger<ClientDeleteCommandHandler> logger,
                                 IMapper mapper,
                                 IClientRepository clientRepository,
                                 IEntityAuthorizationService<Client> clientAuthorizationService) : IRequestHandler<ClientDeleteCommand>
{
    public async Task Handle(ClientDeleteCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete Client {@Client}", request);
        if (!clientAuthorizationService.Authorize(ResourceOperation.Delete)) throw new ForbidException();
        var client = mapper.Map<Client>(request);
        var dbClient = await clientRepository.GetByIDAsync(client.Id);
        if (dbClient is null) throw new NotFoundException(nameof(Client), client.ToString());
        try
        {
            await clientRepository.Delete(dbClient, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Client), client.ToString());
        }
    }
}
