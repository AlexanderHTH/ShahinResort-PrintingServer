using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.ClientReports.Commands;

public class ClientReportCreate : IRequest<Guid>
{
    public Guid ReportId { get; set; }
    public Guid ClientId { get; set; }
    public Guid PrinterId { get; set; }

}
public class ClientReportCreateHandler(ITQLogger<ClientReportCreateHandler> logger,
                                       IMapper mapper,
                                       IClientRepository clientRepository,
                                       IReportRepository reportRepository,
                                       IPrinterRepository printerRepository,
                                       IClientReportRepository clientReportRepository,
                                       IUserContext userContext,
                                       IEntityAuthorizationService<ClientReport> entityAuthorizationService) : IRequestHandler<ClientReportCreate, Guid>
{
    public async Task<Guid> Handle(ClientReportCreate request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create new Cilent-Report");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Create)) throw new ForbidException();
        var client = await clientRepository.GetByIDAsync(request.ClientId) ?? throw new NotFoundException(nameof(ClientReport), string.Format("Client {0} not found.", request.ClientId.ToString()));
        var report = await reportRepository.GetByIDAsync(request.ReportId) ?? throw new NotFoundException(nameof(ClientReport), string.Format("Report {0} not found.", request.ReportId.ToString()));
        var printer = await printerRepository.GetByIDAsync(request.PrinterId) ?? throw new NotFoundException(nameof(ClientReport), string.Format("Printer {0} not found.", request.PrinterId.ToString()));
        var clientreport = mapper.Map<ClientReport>(request);
        var dbClientReport = await clientReportRepository.FindAsync(clientreport);
        if (dbClientReport != null)
        {
            throw new AlreadyFoundException(nameof(ClientReport), dbClientReport.Id.ToString());
        }
        else
        {
            dbClientReport = new ClientReport(userContext.GetCurrentUser()!.Id, report.Id, client.Id, printer.Id, report.ReportName, client.ClientName, client.ClientIp, printer.PrinterName, printer.PrinterIp);
            try
            {
                await clientReportRepository.Create(dbClientReport, cancellationToken);
                return dbClientReport.Id;
            }
            catch
            {
                throw new HandlingDataException(nameof(ClientReport), dbClientReport.Id.ToString());
            }
        }
    }
}