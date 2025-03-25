using AutoMapper;
using PrintingServer.Application.Managment.ClientReports.Commands;
using PrintingServer.Domain.Entities;

namespace PrintingServer.Application.Managment.ClientReports.Dtos;
public class ClientReportProfile : Profile
{
    public ClientReportProfile()
    {
        CreateMap<ClientReportDTO, ClientReport>();
        CreateMap<ClientReport, ClientReportDTO>();
        CreateMap<ClientReportCreate, ClientReport>();
        CreateMap<ClientReportUpdate, ClientReport>();
        CreateMap<ClientReportDeleteCommand, ClientReport>();
    }
}
