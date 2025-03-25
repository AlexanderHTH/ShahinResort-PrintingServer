using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Printers.Commands;
public class PrinterDeleteCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
public class PrinterDeleteCommandHandler(ITQLogger<PrinterDeleteCommandHandler> logger,
                                 IMapper mapper,
                                 IPrinterRepository printerRepository,
                                 IEntityAuthorizationService<Printer> entityAuthorizationService) : IRequestHandler<PrinterDeleteCommand>
{
    public async Task Handle(PrinterDeleteCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleteing Printer {@Printer}", request);
        var printer = mapper.Map<Printer>(request);
        var dbPrinter = await printerRepository.GetByIDAsync(printer.Id) ?? throw new NotFoundException(nameof(Printer), printer.ToString());
        if (!entityAuthorizationService.Authorize(ResourceOperation.Update, dbPrinter)) throw new FormatException();
        try
        {
            await printerRepository.Delete(dbPrinter, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Printer), printer.ToString());
        }
    }
}
