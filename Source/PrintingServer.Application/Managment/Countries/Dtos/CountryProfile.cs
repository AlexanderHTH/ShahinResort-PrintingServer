using AutoMapper;
using PrintingServer.Application.Managment.Countries.Commands;
using PrintingServer.Domain.Entities;

namespace PrintingServer.Application.Managment.Countries.Dtos;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<Country, CountryDTO>();
        CreateMap<CountryDTO, Country>();
        CreateMap<CountryCreateCommand, Country>();
        CreateMap<CountryDeleteCommand, Country>();
        CreateMap<CountryUpdateCommand, Country>();
    }
}
