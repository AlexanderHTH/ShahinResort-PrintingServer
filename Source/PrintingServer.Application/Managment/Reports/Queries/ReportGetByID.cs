using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Managment.Printers.Dtos;
using PrintingServer.Application.Managment.Reports.Dtos;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Reports.Queries;

public class ReportGetByID(Guid id):IRequest<ReportDTO>
{
    public Guid Id { get; } = id;
}
public class ReportGetByIDHandler(ITQLogger<ReportGetByIDHandler> logger,
                                  IMapper mapper,
                                  IReportRepository reportRepository,
                                  IEntityAuthorizationService<Report> entityAuthorizationService) : IRequestHandler<ReportGetByID, ReportDTO>
{
    public async Task<ReportDTO> Handle(ReportGetByID request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting report {ReportID}", request.Id);
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var report = await reportRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(Report), request.Id.ToString());
        var reportDTO = mapper.Map<ReportDTO>(report);
        return reportDTO;

    }
}
