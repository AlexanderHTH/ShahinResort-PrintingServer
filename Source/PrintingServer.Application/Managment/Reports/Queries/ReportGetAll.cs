using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Common;
using PrintingServer.Application.Managment.Reports.Dtos;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Common;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Reports.Queries;

#region Get All
public class ReportGetAll : IRequest<IEnumerable<ReportDTO>>
{

}
public class ReportGetAllHandler(ITQLogger<ReportGetAllHandler> logger,
                                 IMapper mapper,
                                 IReportRepository reportRepository,
                                 IEntityAuthorizationService<Report> entityAuthorizationService) : IRequestHandler<ReportGetAll, IEnumerable<ReportDTO>>
{
    public async Task<IEnumerable<ReportDTO>> Handle(ReportGetAll request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get all reports.");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var reports = (await reportRepository.ListAsync(cancellationToken)).ToList();
        var reportsDTO = mapper.Map<List<ReportDTO>>(reports);
        return reportsDTO;
    }
}
#endregion
#region Get All Active
public class ReportGetAllActive : IRequest<IEnumerable<ReportDTO>>
{

}
public class ReportGetAllActiveHandler(ITQLogger<ReportGetAllActiveHandler> logger,
                                 IMapper mapper,
                                 IReportRepository reportRepository,
                                 IEntityAuthorizationService<Report> entityAuthorizationService) : IRequestHandler<ReportGetAllActive, IEnumerable<ReportDTO>>
{
    public async Task<IEnumerable<ReportDTO>> Handle(ReportGetAllActive request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get all reports.");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var reports = (await reportRepository.ActiveAsync(cancellationToken)).ToList();
        var reportsDTO = mapper.Map<List<ReportDTO>>(reports);
        return reportsDTO;
    }
}
#endregion
#region Search
public class ReportSearch : IRequest<IPagedResult<ReportDTO>>
{
    public required SearchDTO<Report> SearchDTO { get; set; }
}
public class ReportSearchHandler(ITQLogger<ReportSearchHandler> logger,
                                 IMapper mapper,
                                 IReportRepository reportRepository,
                                 IEntityAuthorizationService<Report> entityAuthorizationService) : IRequestHandler<ReportSearch, IPagedResult<ReportDTO>>
{
    public async Task<IPagedResult<ReportDTO>> Handle(ReportSearch request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Search reports.");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var list = await reportRepository.WhereAsync(request.SearchDTO, cancellationToken);
        var toreturn = new PagedResult<ReportDTO>(mapper.Map<List<ReportDTO>>(list), list.TotalItemCount, request.SearchDTO.PageSize, request.SearchDTO.PageNumber);
        return toreturn;
    }
}
#endregion