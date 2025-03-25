using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Common;
using PrintingServer.Application.Managment.Clients.Dtos;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Common;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Clients.Queries;

#region Get All
public class ClientGetAll : IRequest<IEnumerable<ClientDTO>>
{
}
public class ClientGetAllHandler(ITQLogger<ClientGetAllHandler> logger,
                           IMapper mapper,
                           IClientRepository clientRepository,
                           IEntityAuthorizationService<Client> clientAuthorizationService) : IRequestHandler<ClientGetAll, IEnumerable<ClientDTO>>
{
    public async Task<IEnumerable<ClientDTO>> Handle(ClientGetAll request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all clients.");
        if (!clientAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var clients = (await clientRepository.ListAsync(cancellationToken)).ToList();
        var clientsDTO = mapper.Map<List<ClientDTO>>(clients);
        return clientsDTO;
    }
}
#endregion
#region Get All Active
public class ClientGetAllActive : IRequest<IEnumerable<ClientDTO>>
{
}
public class ClientGetAllActiveHandler(ITQLogger<ClientGetAllActiveHandler> logger,
                                       IMapper mapper,
                                       IClientRepository clientRepository,
                                       IEntityAuthorizationService<Client> clientAuthorizationService) : IRequestHandler<ClientGetAllActive, IEnumerable<ClientDTO>>
{
    public async Task<IEnumerable<ClientDTO>> Handle(ClientGetAllActive request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all active clients.");
        if (!clientAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var clients = (await clientRepository.ActiveAsync(cancellationToken)).ToList();
        var clientsDTO = mapper.Map<List<ClientDTO>>(clients);
        return clientsDTO;
    }
}
#endregion
#region General Search
public class ClientSearch : IRequest<IPagedResult<ClientDTO>>
{
    public required SearchDTO<Client> SearchDTO { get; set; }
}
public class ClientSearchHandler(ITQLogger<ClientSearchHandler> logger,
                                 IMapper mapper,
                                 IClientRepository clientRepository,
                                 IEntityAuthorizationService<Client> clientAuthorizationService) : IRequestHandler<ClientSearch, IPagedResult<ClientDTO>>
{
    public async Task<IPagedResult<ClientDTO>> Handle(ClientSearch request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Search clients.");
        if (!clientAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var clients = (await clientRepository.WhereAsync(request.SearchDTO, cancellationToken));
        var toreturn = new PagedResult<ClientDTO>(mapper.Map<List<ClientDTO>>(clients.Items), clients.TotalItemCount, request.SearchDTO.PageSize, request.SearchDTO.PageNumber);
        return toreturn;
    }
}
#endregion