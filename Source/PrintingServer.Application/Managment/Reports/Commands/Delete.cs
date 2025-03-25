using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Managment.Reports.Dtos;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Reports.Commands;
public class ReportDeleteCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
public class ReportDeleteCommandHandler(ITQLogger<ReportDeleteCommandHandler> logger,
                                 IMapper mapper,
                                 IReportRepository reportRepository,
                                 IEntityAuthorizationService<Report> entityAuthorizationService) : IRequestHandler<ReportDeleteCommand>
{
    public async Task Handle(ReportDeleteCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleteing Report {@Report}", request);
        var report = mapper.Map<Report>(request);
        var dbReport = await reportRepository.GetByIDAsync(report.Id) ?? throw new NotFoundException(nameof(Report), report.ToString());
        if (!entityAuthorizationService.Authorize(ResourceOperation.Delete)) throw new ForbidException();
        try
        {
            await reportRepository.Delete(dbReport, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Report), report.ToString());
        }
    }
}
