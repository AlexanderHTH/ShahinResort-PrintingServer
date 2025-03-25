using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Managment.Countries.Dtos;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Countries.Queries;

public class CountryGetByID(Guid id) : IRequest<CountryDTO>
{
    public Guid Id { get; } = id;
}
public class CountryGetByIDHandler(ITQLogger<CountryGetByIDHandler> logger,
                           IMapper mapper,
                           ICountryRepository countryRepository,
                           IEntityAuthorizationService<Country> entityAuthorizationService) : IRequestHandler<CountryGetByID, CountryDTO>
{
    public async Task<CountryDTO> Handle(CountryGetByID request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting country {CountryID}", request.Id);
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var country = await countryRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(Country), request.Id.ToString());
        var countryDTO = mapper.Map<CountryDTO>(country);
        return countryDTO;

    }
}
