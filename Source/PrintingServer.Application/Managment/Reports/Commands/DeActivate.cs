using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Reports.Commands;
public class ReportDeActivate(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
public class ReportDeActivateHandler(ITQLogger<ReportDeActivateHandler> logger,
                                     IReportRepository reportRepository,
                                     IEntityAuthorizationService<Report> entityAuthorizationService) : IRequestHandler<ReportDeActivate>
{
    public async Task Handle(ReportDeActivate request, CancellationToken cancellationToken)
    {
        logger.LogInformation("De-Activate Report {@Report}", request);
        var report = await reportRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(Report), request.Id.ToString());
        if (!entityAuthorizationService.Authorize(ResourceOperation.Update, report)) throw new ForbidException();
        try
        {
            report.Deactivate();
            await reportRepository.Update(report, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Report), report.ToString());
        }
    }
}
