using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Extentions;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Clients.Commands;

public class ClientUpdateCommand : IRequest
{
    public Guid Id { get; set; }
    public string ClientName { get; set; } = default!;
    public string ClientIp { get; set; } = default!;
}
public class ClientUpdateCommandValidator : AbstractValidator<ClientUpdateCommand>
{
    public ClientUpdateCommandValidator()
    {
        RuleFor(dto => dto.ClientName).NotEmpty().WithMessage("Client name is requied>");
        RuleFor(dto => dto.ClientName).Length(3, 100).WithMessage("Client name length must be between (3 - 100)");

        RuleFor(dto => dto.ClientIp).NotEmpty();
        RuleFor(dto => dto.ClientIp)
                .NotEmpty().WithMessage("IP Address is required.")
                .Must(Base.BeAValidIp).WithMessage("Invalid IP Address format.");
    }
}
public class ClientUpdateCommandHandler(ITQLogger<ClientUpdateCommandHandler> logger,
                                 IMapper mapper,
                                 IClientRepository clientRepository,
                                 IUserContext userContext,
                                 IEntityAuthorizationService<Client> clientAuthorizationService) : IRequestHandler<ClientUpdateCommand>
{
    public async Task Handle(ClientUpdateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update Client {@Client}", request);
        var client = mapper.Map<Client>(request);
        var dbClient = await clientRepository.GetByIDAsync(client.Id) ?? throw new NotFoundException(nameof(Client), client.ToString());
        if (!clientAuthorizationService.Authorize(ResourceOperation.Update, client)) throw new ForbidException();
        try
        {
            dbClient.Update(userContext.GetCurrentUser()!.Id, client.ClientName, client.ClientIp);
            await clientRepository.Update(dbClient, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Client), client.ToString());
        }
    }
}
