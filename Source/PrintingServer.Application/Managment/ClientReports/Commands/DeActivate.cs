using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.ClientReports.Commands;
public class ClientReportDeActivateCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
public class ClientReportDeActivateCommandHandler(ITQLogger<ClientReportDeActivateCommandHandler> logger,
                                                  IClientReportRepository clientReportRepository,
                                                  IEntityAuthorizationService<ClientReport> entityAuthorizationService) : IRequestHandler<ClientReportDeActivateCommand>
{
    public async Task Handle(ClientReportDeActivateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeActivate Client-Report {@ClientReport}", request);
        var clientreport = await clientReportRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(ClientReport), request.Id.ToString());
        if (!entityAuthorizationService.Authorize(ResourceOperation.Update ,clientreport)) throw new ForbidException();
        try
        {
                clientreport.Deactivate();
                await clientReportRepository.Update(clientreport, cancellationToken);
            }
            catch
            {
                throw new HandlingDataException(nameof(ClientReport), clientreport.ToString());
            }
        }
}