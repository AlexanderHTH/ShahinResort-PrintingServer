using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;
using PrintingServer.Application.Extentions;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;

namespace PrintingServer.Application.Managment.Clients.Commands;
public class ClientCreateCommand : IRequest<Guid>
{
    public string ClientName { get; set; } = default!;
    public string ClientIp { get; set; } = default!;
}
public class ClientCreateCommandValidator : AbstractValidator<ClientCreateCommand>
{
    public ClientCreateCommandValidator()
    {
        RuleFor(dto => dto.ClientName).NotEmpty().WithMessage("Client name is requied>");
        RuleFor(dto => dto.ClientName).Length(3, 100).WithMessage("Client name length must be between (3 - 100)");

        RuleFor(dto => dto.ClientIp).NotEmpty();
        RuleFor(dto => dto.ClientIp)
                .NotEmpty().WithMessage("IP Address is required.")
                .Must(Base.BeAValidIp).WithMessage("Invalid IP Address format.");
    }
}
public class ClientCreateCommandHandler(ITQLogger<ClientCreateCommandHandler> logger,
                                        IMapper mapper,
                                        IClientRepository clientRepository,
                                        IUserContext userContext,
                                        IEntityAuthorizationService<Client> clientAuthorizationService) : IRequestHandler<ClientCreateCommand, Guid>
{
    public async Task<Guid> Handle(ClientCreateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create new Client {@Client}", request);
        if (!clientAuthorizationService.Authorize(ResourceOperation.Create)) throw new ForbidException();
        var client = mapper.Map<Client>(request);
        if (await clientRepository.FindAsync(client))
        {
            throw new AlreadyFoundException(nameof(Client), client.ToString());
        }
        else
        {
            try
            {
                var tocreate = new Client(userContext.GetCurrentUser()!.Id, request.ClientName, request.ClientIp);
                Guid id = (await clientRepository.Create(tocreate, cancellationToken)).Id;
                return id;
            }
            catch
            {
                throw new HandlingDataException(nameof(Client), client.ToString());
            }
        }
    }
}
