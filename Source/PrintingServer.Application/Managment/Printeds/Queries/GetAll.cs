using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Common;
using PrintingServer.Application.Managment.Printeds.Dtos;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Common;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Printeds.Queries;

#region Get All
public class PrintedGetAll : IRequest<IEnumerable<PrintedDTO>>
{
}
public class PrintedGetAllHandler(ITQLogger<PrintedGetAllHandler> logger,
                                  IMapper mapper,
                                  IPrintedRepository printedRepository,
                                  IEntityAuthorizationService<Printed> entityAuthorizationService) : IRequestHandler<PrintedGetAll, IEnumerable<PrintedDTO>>
{
    public async Task<IEnumerable<PrintedDTO>> Handle(PrintedGetAll request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all printed's.");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var clients = (await printedRepository.ListAsync(cancellationToken)).ToList();
        var clientsDTO = mapper.Map<List<PrintedDTO>>(clients);
        return clientsDTO;
    }
}
#endregion

#region Search
public class PrintedSearch : IRequest<IPagedResult<PrintedDTO>>
{
    public required SearchDTO<Printed> SearchDTO { get; set; }
}
public class PrintedSearchHandler(ITQLogger<PrintedSearchHandler> logger,
                                  IMapper mapper,
                                  IPrintedRepository printedRepository,
                                  IEntityAuthorizationService<Printed> entityAuthorizationService) : IRequestHandler<PrintedSearch, IPagedResult<PrintedDTO>>
{
    public async Task<IPagedResult<PrintedDTO>> Handle(PrintedSearch request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Search printed.");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var list = await printedRepository.WhereAsync(request.SearchDTO, cancellationToken);
        var toreturn = new PagedResult<PrintedDTO>(mapper.Map<List<PrintedDTO>>(list), list.TotalItemCount, request.SearchDTO.PageSize, request.SearchDTO.PageNumber);
        return toreturn;
    }
}
#endregion