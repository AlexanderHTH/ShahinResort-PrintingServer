using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Common;
using PrintingServer.Application.Managment.PrinterErrorLogs.Dtos;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Common;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.PrinterErrorLogs.Queries;

#region Get All
public class PrinterErrorLogGetAll : IRequest<IEnumerable<PrinterErrorLogDTO>>
{
}
public class PrinterErrorLogGetAllHandler(ITQLogger<PrinterErrorLogGetAllHandler> logger,
                                          IMapper mapper,
                                          IPrinterErrorLogRepository printerErrorLogRepository,
                                          IEntityAuthorizationService<Printer> entityAuthorizationService) : IRequestHandler<PrinterErrorLogGetAll, IEnumerable<PrinterErrorLogDTO>>
{
    public async Task<IEnumerable<PrinterErrorLogDTO>> Handle(PrinterErrorLogGetAll request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all printers error logs.");
        if (!entityAuthorizationService.Authorize(Domain.Constants.ResourceOperation.Read)) throw new ForbidException();
        var list = await printerErrorLogRepository.ListAsync(cancellationToken);
        var toreturn = mapper.Map<IEnumerable<PrinterErrorLogDTO>>(list);
        return toreturn;
    }
}
#endregion

#region Search
public class PrinterErrorLogSearch : IRequest<IPagedResult<PrinterErrorLogDTO>>
{
    public required SearchDTO<PrinterErrorLog> SearchDTO { get; set; }
}
public class PrinterErrorLogSearchHandler(ITQLogger<PrinterErrorLogSearchHandler> logger,
                                          IMapper mapper,
                                          IPrinterErrorLogRepository printerErrorLogRepository,
                                          IEntityAuthorizationService<Printer> entityAuthorizationService) : IRequestHandler<PrinterErrorLogSearch, IPagedResult<PrinterErrorLogDTO>>
{
    public async Task<IPagedResult<PrinterErrorLogDTO>> Handle(PrinterErrorLogSearch request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Search printer error logs.");
        if (!entityAuthorizationService.Authorize(Domain.Constants.ResourceOperation.Read)) throw new ForbidException();
        var list = await printerErrorLogRepository.WhereAsync(request.SearchDTO, cancellationToken);
        var toreturn = new PagedResult<PrinterErrorLogDTO>(mapper.Map<List<PrinterErrorLogDTO>>(list), list.TotalItemCount, request.SearchDTO.PageSize, request.SearchDTO.PageNumber);
        return toreturn;
    }
}
#endregion