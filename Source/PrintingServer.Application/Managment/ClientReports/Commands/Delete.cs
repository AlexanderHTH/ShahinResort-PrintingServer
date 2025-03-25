using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.ClientReports.Commands;
public class ClientReportDeleteCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
public class ClientReportDeleteCommandHandler(ITQLogger<ClientReportDeleteCommandHandler> logger,
                                              IMapper mapper,
                                              IClientReportRepository clientReportRepository,
                                              IEntityAuthorizationService<ClientReport> entityAuthorizationService) : IRequestHandler<ClientReportDeleteCommand>
{
    public async Task Handle(ClientReportDeleteCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete Client {@Client}", request);
        if (!entityAuthorizationService.Authorize(ResourceOperation.Delete)) throw new ForbidException();
        var client = mapper.Map<ClientReport>(request);
        var dbClient = await clientReportRepository.GetByIDAsync(client.Id) ?? throw new NotFoundException(nameof(ClientReport), client.ToString());
        try
        {
                await clientReportRepository.Delete(dbClient, cancellationToken);
            }
            catch
            {
                throw new HandlingDataException(nameof(ClientReport), client.ToString());
            }
    }
}
