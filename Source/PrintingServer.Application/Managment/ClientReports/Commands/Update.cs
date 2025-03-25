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
public class ClientReportUpdate : IRequest
{
    public Guid Id { get; set; }
    public Guid ReportId { get; set; }
    public Guid ClientId { get; set; }
    public Guid PrinterId { get; set; }

}
public class ClientReportUpdateHandler(ITQLogger<ClientReportUpdateHandler> logger,
                                       IMapper mapper,
                                       IClientRepository clientRepository,
                                       IReportRepository reportRepository,
                                       IPrinterRepository printerRepository,
                                       IClientReportRepository clientReportRepository,
                                       IUserContext userContext,
                                       IEntityAuthorizationService<ClientReport> entityAuthorizationService) : IRequestHandler<ClientReportUpdate>
{
    public async Task Handle(ClientReportUpdate request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update Cilent-Report {ID}", request.Id.ToString());
        var dbClientReport = await clientReportRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(ClientReport), string.Format("Client-Report {0} not found.", request.Id.ToString()));
        if (!entityAuthorizationService.Authorize(ResourceOperation.Update, dbClientReport)) throw new ForbidException();
        var clientreport = mapper.Map<ClientReport>(request);
        var found = await clientReportRepository.FindAsync(clientreport) ?? throw new AlreadyFoundException(nameof(ClientReport), "Client-Report Updating data already found: " + clientreport.Id.ToString());
        var client = await clientRepository.GetByIDAsync(request.ClientId) ?? throw new NotFoundException(nameof(ClientReport), string.Format("Client {0} not found.", request.ClientId.ToString()));
        var report = await reportRepository.GetByIDAsync(request.ReportId) ?? throw new NotFoundException(nameof(ClientReport), string.Format("Report {0} not found.", request.ReportId.ToString()));
        var printer = await printerRepository.GetByIDAsync(request.PrinterId) ?? throw new NotFoundException(nameof(ClientReport), string.Format("Printer {0} not found.", request.PrinterId.ToString()));
        try
        {
            dbClientReport.Update(userContext.GetCurrentUser()!.Id, report.Id, client.Id, printer.Id, report.ReportName, client.ClientName, client.ClientIp, printer.PrinterName, printer.PrinterIp);
            await clientReportRepository.Update(dbClientReport, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(ClientReport), dbClientReport.ToString());
        }
    }
}