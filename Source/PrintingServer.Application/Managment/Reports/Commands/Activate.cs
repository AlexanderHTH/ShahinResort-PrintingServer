using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Reports.Commands;

public class ReportActivate(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
public class ReportActivateHandler(ITQLogger<ReportActivateHandler> logger,
                                   IReportRepository reportRepository,
                                   IEntityAuthorizationService<Report> entityAuthorizationService) : IRequestHandler<ReportActivate>
{
    public async Task Handle(ReportActivate request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Activate Report {@Report}", request);
        var report = await reportRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(Report), request.Id.ToString());
        if (!entityAuthorizationService.Authorize(ResourceOperation.Update, report)) throw new ForbidException();
        try
        {
            report.Activate();
            await reportRepository.Update(report, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Report), report.ToString());
        }
    }
}
