using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Extentions;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Printers.Commands;
public class PrinterCreateCommand : IRequest<Guid>
{
    public string PrinterName { get; set; } = default!;
    public string PrinterIp { get; set; } = default!;
    public int ResponseTime { get; set; } = 150;
}
public class PrinterCreateCommandValidator : AbstractValidator<PrinterCreateCommand>
{
    public PrinterCreateCommandValidator()
    {
        RuleFor(dto => dto.PrinterName).NotEmpty().WithMessage("Printer name is requied>");
        RuleFor(dto => dto.PrinterName).Length(3, 100).WithMessage("Printer name length must be between(3 - 100)");

        RuleFor(dto => dto.PrinterIp).NotEmpty();
        RuleFor(dto => dto.PrinterIp)
                .NotEmpty().WithMessage("IP Address is required.")
                .Must(Base.BeAValidIp).WithMessage("Invalid IP Address format.");
    }
}
public class PrinterCreateCommandHandler(ITQLogger<PrinterCreateCommandHandler> logger,
                                 IMapper mapper,
                                 IPrinterRepository printerRepository,
                                 IUserContext userContext,
                                 IEntityAuthorizationService<Printer> entityAuthorizationService) : IRequestHandler<PrinterCreateCommand, Guid>
{
    public async Task<Guid> Handle(PrinterCreateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create new Printer {@Printer}", request);
        if (!entityAuthorizationService.Authorize(ResourceOperation.Create)) throw new ForbidException();
        var printer = mapper.Map<Printer>(request);
        if (await printerRepository.FindAsync(printer))
        {
            throw new AlreadyFoundException(nameof(Printer), printer.ToString());
        }
        else
        {
            try
            {
                var tocreate = new Printer(userContext.GetCurrentUser()!.Id, printer.PrinterName, printer.PrinterIp, printer.ResponseTime);
                Guid id = (await printerRepository.Create(tocreate, cancellationToken)).Id;
                return id;
            }
            catch
            {
                throw new HandlingDataException(nameof(Printer), printer.ToString());
            }
        }
    }
}
