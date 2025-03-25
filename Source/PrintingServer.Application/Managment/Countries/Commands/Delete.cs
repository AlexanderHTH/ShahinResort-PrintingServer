using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Countries.Commands;
public class CountryDeleteCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
public class CountryDeleteCommandHandler(ITQLogger<CountryDeleteCommandHandler> logger,
                                 IMapper mapper,
                                 ICountryRepository countryRepository,
                                 IEntityAuthorizationService<Country> entityAuthorizationService) : IRequestHandler<CountryDeleteCommand>
{
    public async Task Handle(CountryDeleteCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete Country {@Country}", request);
        var country = mapper.Map<Country>(request);
        var dbCountry = await countryRepository.GetByIDAsync(country.Id) ?? throw new NotFoundException(nameof(Country), country.ToString());
        if (!entityAuthorizationService.Authorize(ResourceOperation.Delete)) throw new ForbidException();
        try
        {
            await countryRepository.Delete(dbCountry, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Country), country.ToString());
        }
    }
}
