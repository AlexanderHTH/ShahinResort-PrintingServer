using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using PrintingServer.Application.Managment.Clients.Commands;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Entities.UserEntities;
using Xunit;
using static Azure.Core.HttpHeader;

namespace PrintingServer.Application.Managment.Clients.Dtos.Tests;

public class ClientProfileTests
{
    private IMapper mapper;

    public ClientProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ClientProfile>();
        });
        mapper = config.CreateMapper();
    }
    [Fact()]
    public void CreateMap_ClientToClientDTO_OK()
    {

        var client = new Client()
        {
            AppUserId = Guid.NewGuid(),
            ClientIp = "127.0.0.1",
            ClientName = "Test",
            CreatedOn = DateTime.UtcNow,
            Description = " TEST TEST TEST TEST",
            Id = Guid.NewGuid(),
            IsActive = true,
            IsDeleted = false,
            IsModified = false,
            Name = "Test",
            Notes = "",
            UpdatedOn = null,
            //ClientReports = new[]
            //{
            //    new ClientReport()
            //    {
            //        ReportId = Guid.NewGuid(),
            //        AppUserId = Guid.NewGuid(),
            //        CreatedOn = DateTime.UtcNow,
            //        UpdatedOn = null,
            //        Name = "Test Report1",
            //        PrinterId = Guid.NewGuid(),
            //        PrinterIp = "128.0.0.1",
            //        PrinterName = "Test Printer",
            //        ReportName = "Test Report1",
            //        IsActive = true,
            //        IsDeleted = false,
            //        IsModified = false
            //    },
            //    new ClientReport()
            //    {
            //        ReportId = Guid.NewGuid(),
            //        AppUserId = Guid.NewGuid(),
            //        CreatedOn = DateTime.UtcNow,
            //        UpdatedOn = null,
            //        Name = "Test Report2",
            //        PrinterId = Guid.NewGuid(),
            //        PrinterIp = "128.0.0.1",
            //        PrinterName = "Test Printer",
            //        ReportName = "Test Report2",
            //        IsActive = true,
            //        IsDeleted = false,
            //        IsModified = false
            //    }
            //}
        };

        var clientDTO = mapper.Map<ClientDTO>(client);

        clientDTO.Should().NotBeNull();
        clientDTO.AppUserId.Should().Be(client.AppUserId);
        clientDTO.ClientIp.Should().Be(client.ClientIp);
        clientDTO.ClientName.Should().Be(client.ClientName);
        clientDTO.CreatedOn.Should().Be(client.CreatedOn);
        clientDTO.Description.Should().Be(client.Description);
        clientDTO.Id.Should().Be(client.Id);
        clientDTO.IsActive.Should().Be(client.IsActive);
        clientDTO.IsDeleted.Should().Be(client.IsDeleted);
        clientDTO.IsModified.Should().Be(client.IsModified);
        clientDTO.Name.Should().Be(client.Name);
        clientDTO.Notes.Should().Be(client.Notes);
        clientDTO.UpdatedOn.Should().Be(client.UpdatedOn);
    }
    [Fact()]
    public void CreateMap_ClinetDTOToClient_OK()
    {
        var clientDTO = new ClientDTO()
        {
            AppUserId = Guid.NewGuid(),
            ClientIp = "127.0.0.1",
            ClientName = "Test",
            CreatedOn = DateTime.UtcNow,
            Description = " TEST TEST TEST TEST",
            Id = Guid.NewGuid(),
            IsActive = true,
            IsDeleted = false,
            IsModified = false,
            Name = "Test",
            Notes = "",
            UpdatedOn = null
        };

        var client = mapper.Map<Client>(clientDTO);

        client.Should().NotBeNull();
        client.AppUserId.Should().Be(clientDTO.AppUserId);
        client.ClientIp.Should().Be(clientDTO.ClientIp);
        client.ClientName.Should().Be(clientDTO.ClientName);
        client.CreatedOn.Should().Be(clientDTO.CreatedOn);
        client.Description.Should().Be(clientDTO.Description);
        client.Id.Should().Be(clientDTO.Id);
        client.IsActive.Should().Be(clientDTO.IsActive);
        client.IsDeleted.Should().Be(clientDTO.IsDeleted);
        client.IsModified.Should().Be(clientDTO.IsModified);
        client.Name.Should().Be(clientDTO.Name);
        client.Notes.Should().Be(clientDTO.Notes);
        client.UpdatedOn.Should().Be(clientDTO.UpdatedOn);
    }
    [Fact()]
    public void CreateMap_ClinetCreateCommandToClient_OK()
    {
        var create = new ClientCreateCommand()
        {
            ClientIp = "127.0.0.1",
            ClientName = "Test",
        };

        var client = mapper.Map<Client>(create);

        client.Should().NotBeNull();
        client.ClientIp.Should().Be(create.ClientIp);
        client.ClientName.Should().Be(create.ClientName);
    }
    [Fact()]
    public void CreateMap_ClinetUpdateCommandToClient_OK()
    {
        var update = new ClientUpdateCommand()
        {
            Id = Guid.NewGuid(),
            ClientIp = "127.0.0.1",
            ClientName = "Test",
        };

        var client = mapper.Map<Client>(update);

        client.Should().NotBeNull();
        client.Id.Should().Be(update.Id);
        client.ClientIp.Should().Be(update.ClientIp);
        client.ClientName.Should().Be(update.ClientName);
    }
    [Fact()]
    public void CreateMap_ClinetDeleteCommandToClient_OK()
    {
        var delete = new ClientDeleteCommand(Guid.NewGuid());

        var client = mapper.Map<Client>(delete);

        client.Should().NotBeNull();
        client.Id.Should().Be(delete.Id);
    }
}