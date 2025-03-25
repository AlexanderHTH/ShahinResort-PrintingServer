using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using Xunit;

namespace PrintingServer.Application.Managment.Clients.Commands.Tests;

public class ClientCreateCommandHandlerTests
{
    [Fact()]
    public async Task Handle_CreateClient_OKAsync()
    {
        var userContextMock = new Mock<IUserContext>();
        var userID = Guid.NewGuid();
        var currentUser = new CurrentUser(userID, "test", "test@test.com", []);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var loggerMock = new Mock<ITQLogger<ClientCreateCommandHandler>>();
        var mapperMock = new Mock<IMapper>();

        var command = new ClientCreateCommand()
        {
            ClientIp = "127.0.0.1",
            ClientName = "TEST CLIENT"
        };

        var mappedClient = new Client();
        mapperMock.Setup(m => m.Map<Client>(command)).Returns(mappedClient);

        var repoMock = new Mock<IClientRepository>();

        Client? capturedClient = null;
        repoMock.Setup(repo => repo.Create(It.IsAny<Client>(), It.IsAny<CancellationToken>()))
               .Callback<Client, CancellationToken>((c, _) => capturedClient = c)
               .ReturnsAsync(() => capturedClient!);
        var clientAuthorizationServiceMock = new Mock<IEntityAuthorizationService<Client>>();
        clientAuthorizationServiceMock.Setup(m => m.Authorize(ResourceOperation.Create, mappedClient)).Returns(true);

        var commandHandler = new ClientCreateCommandHandler(loggerMock.Object, mapperMock.Object, repoMock.Object, userContextMock.Object, clientAuthorizationServiceMock.Object);

        var result = await commandHandler.Handle(command, CancellationToken.None);

        result.Should().Be(capturedClient!.Id);
        capturedClient!.AppUserId.Should().Be(userID);
        capturedClient!.ClientName.Should().Be(command.ClientName);
        capturedClient!.ClientIp.Should().Be(command.ClientIp);
        repoMock.Verify(c => c.Create(It.Is<Client>(c => c.AppUserId == userID), It.IsAny<CancellationToken>()), Times.Once());

    }
}