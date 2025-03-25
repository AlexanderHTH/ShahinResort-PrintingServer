using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Printers.Commands;
public class PrinterActivateCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
public class PrinterActivateCommandHandler(ITQLogger<PrinterActivateCommandHandler> logger,
                                           IPrinterRepository printerRepository,
                                           IEntityAuthorizationService<Printer> entityAuthorizationService) : IRequestHandler<PrinterActivateCommand>
{
    public async Task Handle(PrinterActivateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Activate Printer {@Printer}", request);
        var printer = await printerRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(Printer), request.Id.ToString());
        if (!entityAuthorizationService.Authorize(ResourceOperation.Update, printer)) throw new ForbidException();
        try
        {
            printer.Activate();
            await printerRepository.Update(printer, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Printer), printer.ToString());
        }
    }
}
