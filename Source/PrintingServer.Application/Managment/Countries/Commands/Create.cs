using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;

namespace PrintingServer.Application.Managment.Countries.Commands;
public class CountryCreateCommand : IRequest<Guid>
{
    public string CountryCode { get; set; } = default!;
    public string CountryEnName { get; set; } = default!;
    public string CountryArName { get; set; } = default!;
    public string CountryEnNationality { get; set; } = default!;
    public string CountryArNationality { get; set; } = default!;
}
public class CountryCreateCommandValidator : AbstractValidator<CountryCreateCommand>
{
    public CountryCreateCommandValidator()
    {
        RuleFor(dto => dto.CountryCode).NotEmpty().WithMessage("Country code is requied>");
        RuleFor(dto => dto.CountryEnName).NotEmpty().WithMessage("Country en-name is requied>");
        RuleFor(dto => dto.CountryArName).NotEmpty().WithMessage("Country ar-name is requied>");
        RuleFor(dto => dto.CountryEnNationality).NotEmpty().WithMessage("Country en-nationality is requied>");
        RuleFor(dto => dto.CountryArNationality).NotEmpty().WithMessage("Country ar-nationality is requied>");
    }
}
public class CountryCreateCommandHandler(ITQLogger<CountryCreateCommandHandler> logger,
                                 IMapper mapper,
                                 ICountryRepository countryRepository,
                                 IEntityAuthorizationService<Country> entityAuthorizationService) : IRequestHandler<CountryCreateCommand, Guid>
{
    public async Task<Guid> Handle(CountryCreateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create new Client {@Client}", request);
        if (!entityAuthorizationService.Authorize(ResourceOperation.Create)) throw new ForbidException();
        var country = mapper.Map<Country>(request);
        if (await countryRepository.FindAsync(country))
        {
            throw new AlreadyFoundException(nameof(Country), country.ToString());
        }
        else
        {
            try
            {
                var tocreate = new Country
                {
                    CountryCode = country.CountryCode,
                    CountryArName = country.CountryArName,
                    CountryArNationality = country.CountryArNationality,
                    CountryEnName = country.CountryEnName,
                    CountryEnNationality = country.CountryEnNationality
                };
                Guid id = (await countryRepository.Create(tocreate, cancellationToken)).Id;
                return id;
            }
            catch
            {
                throw new HandlingDataException(nameof(Country), country.ToString());
            }
        }
    }
}
