using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Managment.Printeds.Dtos;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Printeds.Queries;
public class PrintedGetByID(Guid id) : IRequest<PrintedDTO>
{
    public Guid Id { get; } = id;
}
public class PrintedGetByIDHandler(ITQLogger<PrintedGetByIDHandler> logger,
                                   IMapper mapper,
                                   IPrintedRepository printedRepository,
                                   IEntityAuthorizationService<Printed> entityAuthorizationService) : IRequestHandler<PrintedGetByID, PrintedDTO>
{
    public async Task<PrintedDTO> Handle(PrintedGetByID request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting printed {PrintedID}", request.Id);
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var printer = await printedRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(Printed), request.Id.ToString());
        var printerDTO = mapper.Map<PrintedDTO>(printer);
        return printerDTO;

    }

}
