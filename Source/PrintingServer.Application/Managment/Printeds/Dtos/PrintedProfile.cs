using AutoMapper;
using PrintingServer.Application.Managment.Printeds.Commands;
using PrintingServer.Domain.Entities;

namespace PrintingServer.Application.Managment.Printeds.Dtos
{
    public class PrintedProfile:Profile
    {
        public PrintedProfile()
        {
            CreateMap<Printed, PrintedDTO>();
            CreateMap<PrintedDTO, Printed>();
            CreateMap<PrintedCreateCommand, Printed>();
        }
    }
}
