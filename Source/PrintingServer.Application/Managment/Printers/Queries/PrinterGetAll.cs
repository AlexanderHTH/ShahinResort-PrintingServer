using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Common;
using PrintingServer.Application.Managment.Printers.Dtos;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Common;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Printers.Queries;

#region Gell All
public class PrinterGetAll : IRequest<IEnumerable<PrinterDTO>>
{
}
public class PrinterGetAllHandler(ITQLogger<PrinterGetAllHandler> logger,
                           IMapper mapper,
                           IPrinterRepository printerRepository,
                           IEntityAuthorizationService<Printer> entityAuthorizationService) : IRequestHandler<PrinterGetAll, IEnumerable<PrinterDTO>>
{
    public async Task<IEnumerable<PrinterDTO>> Handle(PrinterGetAll request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all printers.");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new FormatException();
        var clients = (await printerRepository.ListAsync(cancellationToken)).ToList();
        var clientsDTO = mapper.Map<List<PrinterDTO>>(clients);
        return clientsDTO;
    }
}
#endregion
#region Get All Active
public class PrinterGetAllActive : IRequest<IEnumerable<PrinterDTO>>
{
}
public class PrinterGetAllActiveHandler(ITQLogger<PrinterGetAllActiveHandler> logger,
                           IMapper mapper,
                           IPrinterRepository printerRepository,
                           IEntityAuthorizationService<Printer> entityAuthorizationService) : IRequestHandler<PrinterGetAllActive, IEnumerable<PrinterDTO>>
{
    public async Task<IEnumerable<PrinterDTO>> Handle(PrinterGetAllActive request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all active printers.");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new FormatException();
        var toreturn = (await printerRepository.ActiveAsync(cancellationToken)).ToList();
        var toreturnDTO = mapper.Map<List<PrinterDTO>>(toreturn);
        return toreturnDTO;
    }
}
#endregion
#region Search
public class PrinterSearch : IRequest<IPagedResult<PrinterDTO>>
{
    public required SearchDTO<Printer> SearchDTO { get; set; }
}
public class PrinterSearchHandler(ITQLogger<PrinterSearchHandler> logger,
                                  IMapper mapper,
                                  IPrinterRepository printerRepository,
                                  IEntityAuthorizationService<Printer> entityAuthorizationService) : IRequestHandler<PrinterSearch, IPagedResult<PrinterDTO>>
{
    public async Task<IPagedResult<PrinterDTO>> Handle(PrinterSearch request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Search printer.");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new FormatException();
        var list = await printerRepository.WhereAsync(request.SearchDTO, cancellationToken);
        var toreturn = new PagedResult<PrinterDTO>(mapper.Map<List<PrinterDTO>>(list), list.TotalItemCount, request.SearchDTO.PageSize, request.SearchDTO.PageNumber);
        return toreturn;
    }
}
#endregion