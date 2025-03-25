using AutoMapper;
using PrintingServer.Application.Managment.Reports.Commands;
using PrintingServer.Domain.Entities;

namespace PrintingServer.Application.Managment.Reports.Dtos;

public class ReportProfile : Profile
{
    public ReportProfile()
    {
        CreateMap<Report, ReportDTO>();
        CreateMap<ReportDTO, Report>();
        CreateMap<ReportCreate, Report>();
        CreateMap<ReportUpdate, Report>();
        CreateMap<ReportDeleteCommand, Report>();
        CreateMap<ReportActivate, Report>();
        CreateMap<ReportDeActivate, Report>();
    }
}
