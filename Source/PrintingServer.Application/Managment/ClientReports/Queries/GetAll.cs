using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Common;
using PrintingServer.Application.Managment.ClientReports.Commands;
using PrintingServer.Application.Managment.ClientReports.Dtos;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Common;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.ClientReports.Queries;

#region Get All
public class ClientReportGetAll : IRequest<IEnumerable<ClientReportDTO>>
{
}
public class ClientReportGetAllHandler(ITQLogger<ClientReportActivateCommandHandler> logger,
                                       IMapper mapper,
                                       IClientReportRepository clientReportRepository,
                                       IEntityAuthorizationService<ClientReport> entityAuthorizationService) : IRequestHandler<ClientReportGetAll, IEnumerable<ClientReportDTO>>
{
    public async Task<IEnumerable<ClientReportDTO>> Handle(ClientReportGetAll request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get all client-reports.");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var list = (await clientReportRepository.ListAsync(cancellationToken));//.Where(q =>
        var toreturn = mapper.Map<List<ClientReportDTO>>(list);
        return toreturn;
    }
}
#endregion
#region Get All Active
public class ClientReportGetAllActive : IRequest<IEnumerable<ClientReportDTO>>
{
}
public class ClientReportGetAllActiveHandler(ITQLogger<ClientReportGetAllActiveHandler> logger,
                                             IMapper mapper,
                                             IClientReportRepository clientReportRepository,
                                             IEntityAuthorizationService<ClientReport> entityAuthorizationService) : IRequestHandler<ClientReportGetAllActive, IEnumerable<ClientReportDTO>>
{
    public async Task<IEnumerable<ClientReportDTO>> Handle(ClientReportGetAllActive request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get all active client-reports.");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var list = await clientReportRepository.ActiveAsync(cancellationToken);
        var toreturn = mapper.Map<List<ClientReportDTO>>(list);
        return toreturn;
    }
}
#endregion
#region Search
public class ClientReportSearch : IRequest<IPagedResult<ClientReportDTO>>
{
    public required SearchDTO<ClientReport> SearchDTO { get; set; }
}
public class ClientReportSearchHandler(ITQLogger<ClientReportSearchHandler> logger,
                                       IMapper mapper,
                                       IClientReportRepository clientReportRepository,
                                       IEntityAuthorizationService<ClientReport> entityAuthorizationService) : IRequestHandler<ClientReportSearch, IPagedResult<ClientReportDTO>>
{
    public async Task<IPagedResult<ClientReportDTO>> Handle(ClientReportSearch request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Search client-reports.");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var list = await clientReportRepository.WhereAsync(request.SearchDTO, cancellationToken);
        var toreturn = new PagedResult<ClientReportDTO>(mapper.Map<List<ClientReportDTO>>(list), list.TotalItemCount, request.SearchDTO.PageSize, request.SearchDTO.PageNumber);
        return toreturn;
    }
}
#endregion