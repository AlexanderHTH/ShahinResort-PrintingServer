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

namespace PrintingServer.Application.Managment.Printeds.Commands;
public class PrintedCreateCommand : IRequest<Guid>
{
    public Guid ClientReportId { get; set; }
    public DateTime PrintedTime { get; set; }
    public string MainObjectGuid { get; set; } = default!;
    public string PrintedBy { get; set; } = default!;
}
public class PrintedCreateCommandValidator : AbstractValidator<PrintedCreateCommand>
{
    public PrintedCreateCommandValidator()
    {
        RuleFor(dto => dto.MainObjectGuid).NotEmpty().WithMessage("Main object ID is required.");
        RuleFor(dto => dto.MainObjectGuid).Must(Base.BeAValidGuid)
                .NotEmpty().WithMessage("IP Address is required.").WithMessage("Invalid object ID format.");

        RuleFor(dto => dto.PrintedBy).NotEmpty().WithMessage("PrintedBy is required.");
    }
}
public class PrintedCreateCommandHandler(ITQLogger<PrintedCreateCommandHandler> logger,
                                         IMapper mapper,
                                         IClientReportRepository clientReportRepository,
                                         IPrintedRepository printedRepository,
                                         IUserContext userContext,
                                         IEntityAuthorizationService<Printed> entityAuthorizationService) : IRequestHandler<PrintedCreateCommand, Guid>
{
    public async Task<Guid> Handle(PrintedCreateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create new Printed {@Printer}", request);
        if (!entityAuthorizationService.Authorize(ResourceOperation.Create)) throw new ForbidException();
        var printer = mapper.Map<Printer>(request);
        try
        {
            var clientReport = await clientReportRepository.GetByIDAsync(request.ClientReportId) ?? throw new NotFoundException(nameof(Printed), "Client-Report {0} not found." + request.ClientReportId.ToString());
            var tocreate = new Printed(userContext.GetCurrentUser()!.Id, request.ClientReportId, request.PrintedTime, request.MainObjectGuid, request.PrintedBy);
            Guid id = (await printedRepository.Create(tocreate, cancellationToken)).Id;
            return id;
        }
        catch
        {
            throw new HandlingDataException(nameof(Printer), printer.ToString());
        }
    }
}
