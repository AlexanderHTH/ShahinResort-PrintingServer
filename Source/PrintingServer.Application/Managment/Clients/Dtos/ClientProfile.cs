using AutoMapper;
using PrintingServer.Application.Managment.Clients.Commands;
using PrintingServer.Domain.Entities;

namespace PrintingServer.Application.Managment.Clients.Dtos;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<ClientCreateCommand, Client>()
            .ForMember(b => b.ClientName, opt => opt.MapFrom(src => src.ClientName))
            .ForMember(b => b.ClientIp, opt => opt.MapFrom(src => src.ClientIp));
        CreateMap<ClientUpdateCommand, Client>()
            .ForMember(b => b.ClientName, opt => opt.MapFrom(src => src.ClientName))
            .ForMember(b => b.ClientIp, opt => opt.MapFrom(src => src.ClientIp))
            .ForMember(b => b.Id, opt => opt.MapFrom(src => src.Id));
        CreateMap<ClientDeleteCommand, Client>();
        CreateMap<ClientDTO, Client>();
        CreateMap<Client, ClientDTO>()
                .ForMember(b => b.ClientIp, opt => opt.MapFrom(src => src.ClientIp))
                .ForMember(b => b.ClientName, opt => opt.MapFrom(src => src.ClientName))
                .ForMember(b => b.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(b => b.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(b => b.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(b => b.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(b => b.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(b => b.UpdatedOn, opt => opt.MapFrom(src => src.UpdatedOn))
                .ForMember(b => b.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(b => b.IsModified, opt => opt.MapFrom(src => src.IsModified))
                .ForMember(b => b.IsActive, opt => opt.MapFrom(src => src.IsActive));
        //.ForMember(b => b.ClientReports, opt => opt.MapFrom(src => src.ClientReports));
    }
}
