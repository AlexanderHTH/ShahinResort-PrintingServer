using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.PrinterErrorLogs.Commands;

public class PrinterErrorLogCreate : IRequest<Guid>
{
    public Guid PrinterId { get; set; }
    public DateTime ErrorDate { get; set; }
    public string Details { get; set; } = default!;
}
public class PrinterErrorLogCreateHandler(ITQLogger<PrinterErrorLogCreateHandler> logger,
                                 IMapper mapper,
                                 IPrinterRepository printerRepository,
                                 IPrinterErrorLogRepository printerErrorLogRepository,
                                 IUserContext userContext,
                                 IEntityAuthorizationService<PrinterErrorLog> entityAuthorizationService) : IRequestHandler<PrinterErrorLogCreate, Guid>
{

    public async Task<Guid> Handle(PrinterErrorLogCreate request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create new Printer Error Log {@ErrorLog}", request);
        if (!entityAuthorizationService.Authorize(ResourceOperation.Create)) throw new ForbidException();
        var errorlog = mapper.Map<PrinterErrorLog>(request);
        var printer = await printerRepository.GetByIDAsync(errorlog.PrinterId) ?? throw new NotFoundException(nameof(PrinterErrorLog), string.Format("Printer {0} not found", errorlog.PrinterId.ToString()));
        try
        {
            var tocreate = new PrinterErrorLog(userContext.GetCurrentUser()!.Id, errorlog.PrinterId, errorlog.ErrorDate, errorlog.Details);
            Guid id = (await printerErrorLogRepository.Create(tocreate, cancellationToken)).Id;
            return id;
        }
        catch
        {
            throw new HandlingDataException(nameof(PrinterErrorLog), errorlog.ToString());
        }
    }
}
