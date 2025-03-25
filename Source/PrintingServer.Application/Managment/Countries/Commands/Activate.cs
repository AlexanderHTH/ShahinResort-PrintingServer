using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Countries.Commands;
public class CountryActivateCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
public class CountryActivateCommandHandler(ITQLogger<CountryActivateCommandHandler> logger,
                                 ICountryRepository countryRepository,
                                 IEntityAuthorizationService<Country> entityAuthorizationService) : IRequestHandler<CountryActivateCommand>
{
    public async Task Handle(CountryActivateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Activate Country {@Country}", request);
        var country = await countryRepository.GetByIDAsync(request.Id) ?? throw new NotFoundException(nameof(Country), request.Id.ToString());
        if (!entityAuthorizationService.Authorize(ResourceOperation.Update, country)) throw new ForbidException();
        try
        {
            country.Activate();
            await countryRepository.Update(country, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Country), country.ToString());
        }
    }
}
