using AutoMapper;
using PrintingServer.Application.Managment.PrinterErrorLogs.Commands;
using PrintingServer.Domain.Entities;

namespace PrintingServer.Application.Managment.PrinterErrorLogs.Dtos;

public class PrinterErrorLogProfile : Profile
{
    public PrinterErrorLogProfile()
    {
        CreateMap<PrinterErrorLog, PrinterErrorLogDTO>();
        CreateMap<PrinterErrorLogDTO, PrinterErrorLog>();
        CreateMap<PrinterErrorLogCreate, PrinterErrorLog>();
    }
}
