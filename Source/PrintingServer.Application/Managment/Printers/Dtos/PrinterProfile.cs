using AutoMapper;
using PrintingServer.Application.Managment.Printers.Commands;
using PrintingServer.Domain.Entities;

namespace PrintingServer.Application.Managment.Printers.Dtos;

public class PrinterProfile : Profile
{
    public PrinterProfile()
    {
        CreateMap<PrinterCreateCommand, Printer>();
        CreateMap<PrinterUpdateCommand, Printer>();
        CreateMap<PrinterDeleteCommand, Printer>();
        CreateMap<Printer, PrinterDTO>();
        CreateMap<PrinterDTO, Printer>();
    }
}
