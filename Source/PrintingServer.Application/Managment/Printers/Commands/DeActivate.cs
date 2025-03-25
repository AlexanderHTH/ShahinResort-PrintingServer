using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Printers.Commands;
public class PrinterDeActivateCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
public class PrinterDeActivateCommandHandler(ITQLogger<PrinterDeActivateCommandHandler> logger,
                                             IPrinterRepository printerRepository,
                                             IEntityAuthorizationService<Printer> entityAuthorizationService) : IRequestHandler<PrinterDeActivateCommand>
{
    public async Task Handle(PrinterDeActivateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("De-Activate Printer {@Printer}", request);
        var printer = await printerRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(Printer), request.Id.ToString());
        if (!entityAuthorizationService.Authorize(ResourceOperation.Update, printer)) throw new FormatException();
        try
        {
            printer.Deactivate();
            await printerRepository.Update(printer, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Printer), printer.ToString());
        }
    }
}
