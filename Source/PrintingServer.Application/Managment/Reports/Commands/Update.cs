using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Reports.Commands;
public class ReportUpdate : IRequest
{
    public Guid Id { get; set; }
    public Guid PrinterId { get; set; }
    public string ReportName { get; set; } = default!;
    public string ReportPath { get; set; } = default!;
}
public class ReportUpdateValidator : AbstractValidator<ReportUpdate>
{
    public ReportUpdateValidator()
    {
        RuleFor(dto => dto.PrinterId).NotEmpty().NotNull().WithMessage("Printer ID Requied.");
        RuleFor(dto => dto.ReportName).NotEmpty().WithMessage("Report name Requied.");
        RuleFor(dto => dto.ReportName).Length(3, 100).WithMessage("Report name length must be between (3 - 100)");
    }
}
public class ReportUpdateHandler(ITQLogger<ReportUpdateHandler> logger,
                                 IMapper mapper,
                                 IReportRepository reportRepository,
                                 IUserContext userContext,
                                 IEntityAuthorizationService<Report> entityAuthorizationService) : IRequestHandler<ReportUpdate>
{

    public async Task Handle(ReportUpdate request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update Report {@Report}", request);
        var report = mapper.Map<Report>(request);
        var dbReport = await reportRepository.GetByIDAsync(report.Id) ?? throw new NotFoundException(nameof(Report), report.ToString());
        if (!entityAuthorizationService.Authorize(ResourceOperation.Update, report)) throw new ForbidException();
        try
        {
            dbReport.Update(userContext.GetCurrentUser()!.Id, report.PrinterId, report.ReportName, report.ReportPath);
            await reportRepository.Update(dbReport, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Printer), report.ToString());
        }
    }
}
