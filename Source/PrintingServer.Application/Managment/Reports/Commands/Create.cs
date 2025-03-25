using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Managment.Reports.Dtos;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Reports.Commands;

public class ReportCreate : IRequest<Guid>
{
    public Guid PrinterId { get; set; }
    public string ReportName { get; set; } = default!;
    public string ReportPath { get; set; } = default!;
}
public class ReportCreateValidator : AbstractValidator<ReportCreate>
{
    public ReportCreateValidator()
    {
        RuleFor(dto => dto.PrinterId).NotEmpty().NotNull().WithMessage("Printer ID Requied.");
        RuleFor(dto => dto.ReportName).NotEmpty().WithMessage("Report name Requied.");
        RuleFor(dto => dto.ReportName).Length(3, 100).WithMessage("Report name length must be between (3 - 100)");
    }
}
public class ReportCreateHandler(ITQLogger<ReportCreateHandler> logger,
                                 IMapper mapper,
                                 IReportRepository reportRepository,
                                 IUserContext userContext,
                                 IEntityAuthorizationService<Report> entityAuthorizationService) : IRequestHandler<ReportCreate, Guid>
{

    public async Task<Guid> Handle(ReportCreate request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create new Report {@Report}", request);
        if (!entityAuthorizationService.Authorize(ResourceOperation.Create)) throw new ForbidException();
        var report = mapper.Map<Report>(request);
        if (await reportRepository.FindAsync(report))
        {
            throw new AlreadyFoundException(nameof(Report), report.ToString());
        }
        else
        {
            try
            {
                var tocreate = new Report(userContext.GetCurrentUser()!.Id, report.PrinterId, report.ReportName, report.ReportPath);
                Guid id = (await reportRepository.Create(tocreate, cancellationToken)).Id;
                return id;
            }
            catch
            {
                throw new HandlingDataException(nameof(Report), report.ToString());
            }
        }
    }
}
