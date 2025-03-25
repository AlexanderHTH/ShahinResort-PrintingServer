using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Managment.Clients.Dtos;
using PrintingServer.Application.Managment.Reports.Dtos;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Clients.Queries;

public class ClientGetByID(Guid id) : IRequest<ClientDTO>
{
    public Guid Id { get; } = id;
}
public class ClientGetByIDHandler(ITQLogger<ClientGetByIDHandler> logger,
                           IMapper mapper,
                           IClientRepository clientRepository,
                           IEntityAuthorizationService<Client> entityAuthorizationService) : IRequestHandler<ClientGetByID, ClientDTO>
{
    public async Task<ClientDTO> Handle(ClientGetByID request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting clinet {ClientID}", request.Id);
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var client = await clientRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(Client), request.Id.ToString());
        var clientDTO = mapper.Map<ClientDTO>(client);
        return clientDTO;

    }
}

public class ClientAllowedReports(Guid id) : IRequest<IEnumerable<ReportDTO>>
{
    public Guid Id { get; } = id;
}
public class ClientAllowedReportsHandler(ITQLogger<ClientAllowedReports> logger,
                           IMapper mapper,
                           IClientRepository clientRepository,
                           IReportRepository reportRepository,
                           IEntityAuthorizationService<Client> entityAuthorizationService) : IRequestHandler<ClientAllowedReports, IEnumerable<ReportDTO>>
{
    public async Task<IEnumerable<ReportDTO>> Handle(ClientAllowedReports request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting allowed reports for clinet {ClientID}", request.Id);
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var client = await clientRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(Client), request.Id.ToString());
        List<ReportDTO> result = [];
        foreach (var cr in client.ClientReports)
        {
            result.Add(mapper.Map<ReportDTO>(await reportRepository.GetByIDAsync(cr.ReportId, false)));
        }
        return result;
    }
}
