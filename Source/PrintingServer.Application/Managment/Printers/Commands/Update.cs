using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Extentions;
using PrintingServer.Application.Managment.Printers.Dtos;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;
using System.Text.RegularExpressions;

namespace PrintingServer.Application.Managment.Printers.Commands;
public class PrinterUpdateCommand : IRequest
{
    public Guid Id { get; set; }
    public string PrinterName { get; set; } = default!;
    public string PrinterIp { get; set; } = default!;
    public int ResponseTime { get; set; } = 150;
}
public class PrinterUpdateCommandValidator : AbstractValidator<PrinterUpdateCommand>
{
    public PrinterUpdateCommandValidator()
    {
        RuleFor(dto => dto.PrinterName).NotEmpty().WithMessage("Printer name is requied>");
        RuleFor(dto => dto.PrinterName).Length(3, 100).WithMessage("Printer name is requied>");

        RuleFor(dto => dto.PrinterIp).NotEmpty();
        RuleFor(dto => dto.PrinterIp)
                .NotEmpty().WithMessage("IP Address is required.")
                .Must(Base.BeAValidIp).WithMessage("Invalid IP Address format.");
    }
}
public class PrinterUpdateCommandHandler(ITQLogger<PrinterUpdateCommandHandler> logger,
                                 IMapper mapper,
                                 IPrinterRepository printerRepository,
                                 IUserContext userContext,
                                 IEntityAuthorizationService<Printer> entityAuthorizationService) : IRequestHandler<PrinterUpdateCommand>
{
    public async Task Handle(PrinterUpdateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update Priner {@Priner}", request);
        var printer = mapper.Map<Printer>(request);
        var dbPrinter = await printerRepository.GetByIDAsync(printer.Id) ?? throw new NotFoundException(nameof(Printer), printer.ToString());
        if (!entityAuthorizationService.Authorize(ResourceOperation.Update, dbPrinter)) throw new FormatException();
        try
        {
            dbPrinter.Update(userContext.GetCurrentUser()!.Id, printer.PrinterName, printer.PrinterIp, printer.ResponseTime);
            await printerRepository.Update(dbPrinter, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Printer), printer.ToString());
        }
    }
}
