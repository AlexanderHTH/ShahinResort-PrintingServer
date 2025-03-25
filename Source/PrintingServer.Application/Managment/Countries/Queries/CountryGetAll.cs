using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.Common;
using PrintingServer.Application.Managment.Countries.Dtos;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Common;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Countries.Queries;

#region Get All
public class CountryGetAll : IRequest<List<CountryDTO>>
{

}
public class CountryGetAllHandler(ITQLogger<CountryGetAllHandler> logger,
                           IMapper mapper,
                           ICountryRepository countryRepository,
                           IEntityAuthorizationService<Country> entityAuthorizationService) : IRequestHandler<CountryGetAll, List<CountryDTO>>

{
    public async Task<List<CountryDTO>> Handle(CountryGetAll request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all countries.");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var toretrun = (await countryRepository.ListAsync(cancellationToken));
        var toretrunDTO = mapper.Map<List<CountryDTO>>(toretrun);
        return toretrunDTO;
    }
}
#endregion
#region Get All Active
public class CountryGetAllActive : IRequest<IEnumerable<CountryDTO>>
{
}
public class CountryGetAllActiveHandler(ITQLogger<CountryGetAllActiveHandler> logger,
                           IMapper mapper,
                           ICountryRepository countryRepository,
                           IEntityAuthorizationService<Country> entityAuthorizationService) : IRequestHandler<CountryGetAllActive, IEnumerable<CountryDTO>>
{
    public async Task<IEnumerable<CountryDTO>> Handle(CountryGetAllActive request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all active countries.");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var countries = (await countryRepository.ActiveAsync(cancellationToken)).ToList();
        var countriesDTO = mapper.Map<List<CountryDTO>>(countries);
        return countriesDTO;
    }
}
#endregion
#region Search
public class CountrySearch : IRequest<IPagedResult<CountryDTO>>
{
    public required SearchDTO<Country> SearchDTO { get; set; }
}
public class CountrySearchHandler(ITQLogger<CountrySearchHandler> logger,
                                  IMapper mapper,
                                  ICountryRepository countryRepository,
                                  IEntityAuthorizationService<Country> entityAuthorizationService) : IRequestHandler<CountrySearch, IPagedResult<CountryDTO>>
{
    public async Task<IPagedResult<CountryDTO>> Handle(CountrySearch request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Search countries.");
        if (!entityAuthorizationService.Authorize(ResourceOperation.Read)) throw new ForbidException();
        var list = await countryRepository.WhereAsync(request.SearchDTO, cancellationToken);
        var toreturn = new PagedResult<CountryDTO>(mapper.Map<List<CountryDTO>>(list), list.TotalItemCount, request.SearchDTO.PageSize, request.SearchDTO.PageNumber);
        return toreturn;
    }
}
#endregion