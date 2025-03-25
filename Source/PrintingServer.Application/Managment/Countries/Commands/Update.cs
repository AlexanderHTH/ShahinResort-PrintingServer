using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Countries.Commands;
public class CountryUpdateCommand : IRequest
{
    public Guid Id { get; set; }
    public string CountryCode { get; set; } = default!;
    public string CountryEnName { get; set; } = default!;
    public string CountryArName { get; set; } = default!;
    public string CountryEnNationality { get; set; } = default!;
    public string CountryArNationality { get; set; } = default!;
}
public class CountryUpdateCommandValidator : AbstractValidator<CountryUpdateCommand>
{
    public CountryUpdateCommandValidator()
    {
        RuleFor(dto => dto.CountryCode).NotEmpty().WithMessage("Country code is requied.");
        RuleFor(dto => dto.CountryEnName).NotEmpty().WithMessage("Country en-name is requied.");
        RuleFor(dto => dto.CountryArName).NotEmpty().WithMessage("Country ar-name is requied.");
        RuleFor(dto => dto.CountryEnNationality).NotEmpty().WithMessage("Country en-nationality is requied.");
        RuleFor(dto => dto.CountryArNationality).NotEmpty().WithMessage("Country ar-nationality is requied.");
    }
}
public class CountryUpdateCommandHandler(ITQLogger<CountryUpdateCommandHandler> logger,
                                 IMapper mapper,
                                 ICountryRepository countryRepository,
                                 IUserContext userContext,
                                 IEntityAuthorizationService<Country> entityAuthorizationService) : IRequestHandler<CountryUpdateCommand>
{
    public async Task Handle(CountryUpdateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update Country {@Country}", request);
        var country = mapper.Map<Country>(request);
        var dbCountry = await countryRepository.GetByIDAsync(country.Id) ?? throw new NotFoundException(nameof(Country), country.ToString());
        if (!entityAuthorizationService.Authorize(ResourceOperation.Update, dbCountry)) throw new ForbidException();
        try
        {
            dbCountry.Update(userContext.GetCurrentUser()!.Id, country.CountryCode, country.CountryEnName, country.CountryArName, country.CountryEnNationality, country.CountryArNationality);
            await countryRepository.Update(dbCountry, cancellationToken);
        }
        catch
        {
            throw new HandlingDataException(nameof(Country), country.ToString());
        }
    }
}
