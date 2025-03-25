using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Managment.ClientReports.Dtos;
using PrintingServer.Application.Managment.Printeds.Dtos;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.ClientReports.Queries;

public class ClientReportGetByID(Guid id) : IRequest<ClientReportDTO>
{
    public Guid Id { get; } = id;
}
public class ClientReportGetByIDHandler(ITQLogger<ClientReportGetByIDHandler> logger,
                                        IMapper mapper,
                                        IClientReportRepository clientReportRepository,
                                        IEntityAuthorizationService<ClientReport> entityAuthorizationService) : IRequestHandler<ClientReportGetByID, ClientReportDTO>
{
    public async Task<ClientReportDTO> Handle(ClientReportGetByID request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting clinet-report {ClientReportID}", request.Id);
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var client = await clientReportRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(ClientReport), request.Id.ToString());
        var clientDTO = mapper.Map<ClientReportDTO>(client);
        return clientDTO;
    }
}

public class ClientReportGetAllPrinted(Guid id) : IRequest<IEnumerable<PrintedDTO>>
{
    public Guid Id { get; } = id;
}
public class ClientReportGetAllPrintedHandler(ITQLogger<ClientReportGetAllHandler> logger,
                                       IMapper mapper,
                                       IClientReportRepository clientReportRepository,
                                       IEntityAuthorizationService<ClientReport> entityAuthorizationService) : IRequestHandler<ClientReportGetAllPrinted, IEnumerable<PrintedDTO>>
{
    public async Task<IEnumerable<PrintedDTO>> Handle(ClientReportGetAllPrinted request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all printed clinet-report {ClientReportID}", request.Id);
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var client = await clientReportRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(ClientReport), request.Id.ToString());
        return mapper.Map<List<PrintedDTO>>(client.Printeds);
    }
}
