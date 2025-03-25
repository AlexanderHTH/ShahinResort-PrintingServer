using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Managment.PrinterErrorLogs.Dtos;
using PrintingServer.Application.Managment.Printers.Dtos;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Printers.Queries;
public class PrinterGetByID(Guid id) : IRequest<PrinterDTO>
{
    public Guid Id { get; } = id;
}
public class PrinterGetByIDHandler(ITQLogger<PrinterGetByIDHandler> logger,
                           IMapper mapper,
                           IPrinterRepository printerRepository,
                           IEntityAuthorizationService<Printer> entityAuthorizationService) : IRequestHandler<PrinterGetByID, PrinterDTO>
{
    public async Task<PrinterDTO> Handle(PrinterGetByID request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting printer {PrinterID}", request.Id);
        var printer = await printerRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(Printer), request.Id.ToString());
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read, printer)) throw new ForbidException();
        var printerDTO = mapper.Map<PrinterDTO>(printer);
        return printerDTO;

    }
}
public class PrinterGetErrorLogs(Guid id) : IRequest<IEnumerable<PrinterErrorLogDTO>>
{
    public Guid Id { get; } = id;
}
public class PrinterGetErrorLogsHandler(ITQLogger<PrinterGetErrorLogsHandler> logger,
                           IMapper mapper,
                           IPrinterRepository printerRepository,
                           IEntityAuthorizationService<Printer> entityAuthorizationService) : IRequestHandler<PrinterGetErrorLogs, IEnumerable<PrinterErrorLogDTO>>
{
    public async Task<IEnumerable<PrinterErrorLogDTO>> Handle(PrinterGetErrorLogs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting printer {PrinterID}", request.Id);
        var printer = await printerRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(Printer), request.Id.ToString());
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read, printer)) throw new ForbidException();
        var logs = mapper.Map<IEnumerable<PrinterErrorLogDTO>>(printer.PrinterErrorLogs);
        return logs;
    }
}
