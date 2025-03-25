using AutoMapper;
using Azure.Identity;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Repositories;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PrintingServer.Application.Managment.Clients.Commands.Tests;

public class ClientUpdateCommandHandlerTests
{
    private readonly Mock<ITQLogger<ClientUpdateCommandHandler>> loggerMock;
    private readonly Mock<IClientRepository> clientRepositoryMock;
    private readonly Mock<IMapper> mapperMock;
    private readonly Mock<IUserContext> userContextMock;
    private readonly Mock<IEntityAuthorizationService<Client>> clientAuthorizationServiceMock;
    private readonly ClientUpdateCommandHandler handler;
    public ClientUpdateCommandHandlerTests()
    {
        userContextMock = new Mock<IUserContext>();
        loggerMock = new Mock<ITQLogger<ClientUpdateCommandHandler>>();
        mapperMock = new Mock<IMapper>();
        clientRepositoryMock = new Mock<IClientRepository>();
        clientAuthorizationServiceMock = new Mock<IEntityAuthorizationService<Client>>();
        handler = new ClientUpdateCommandHandler(loggerMock.Object, mapperMock.Object, clientRepositoryMock.Object, userContextMock.Object, clientAuthorizationServiceMock.Object);

    }
    [Fact()]
    public async Task Handle_UpdateClient_OKAsync()
    {
        var userID = Guid.NewGuid();
        var currentUser = new CurrentUser(userID, "test@test.com", "test", []);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);
        var clientID = Guid.NewGuid();
        var client = new Client()
        {
            Id = clientID,
            ClientName = "TEST TEST TEST",
            ClientIp = "127.0.0.4"
        };
        var command = new ClientUpdateCommand()
        {
            Id = clientID,
            ClientName = "UPDATE UPDATE",
            ClientIp = "127.0.0.5"
        };
        //var mappedClient = new Client();
        mapperMock.Setup(m => m.Map<Client>(command)).Returns(client);
        clientRepositoryMock.Setup(r => r.GetByIDAsync(clientID, true)).ReturnsAsync(client);
        //Client? capturedClient = null;
        //clientRepositoryMock.Setup(repo => repo.Create(It.IsAny<Client>(), It.IsAny<CancellationToken>()))
        //       .Callback<Client, CancellationToken>((c, _) => capturedClient = c)
        //       .ReturnsAsync(() => capturedClient!);
        clientAuthorizationServiceMock.Setup(m => m.Authorize(ResourceOperation.Update, client)).Returns(true);

        await handler.Handle(command, CancellationToken.None);

        clientRepositoryMock.Verify(r => r.Update(client, CancellationToken.None), Times.Once);
        //mapperMock.Verify(m => m.Map(command, client), Times.Once);
    }
    [Fact()]
    public async Task Handle_WithNotExistingClient_ThrowNotFound()
    {
        var userID = Guid.NewGuid();
        var currentUser = new CurrentUser(userID, "test@test.com", "test", []);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);
        var clientID = Guid.NewGuid();
        var client = new Client()
        {
            Id = clientID,
            ClientName = "TEST TEST TEST",
            ClientIp = "127.0.0.4"
        };
        var command = new ClientUpdateCommand()
        {
            Id = clientID,
            ClientName = "UPDATE UPDATE",
            ClientIp = "127.0.0.5"
        };
        mapperMock.Setup(m => m.Map<Client>(command)).Returns(client);
        clientRepositoryMock.Setup(r => r.GetByIDAsync(clientID, true)).ReturnsAsync((Client?)null);
        
        Func<Task> act = async() => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();//.WithMessage($"Client with Id: {clientID} not found.");
    }
    [Fact()]
    public async Task Handle_WithUnauthorizedUser_ThrowForbidden()
    {
        var userID = Guid.NewGuid();
        var currentUser = new CurrentUser(userID, "test@test.com", "test", []);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);
        var clientID = Guid.NewGuid();
        var request = new ClientUpdateCommand()
        {
            Id = clientID
        };
        var existingClinet = new Client
        {
            Id = clientID
        };
        mapperMock.Setup(m => m.Map<Client>(request)).Returns(existingClinet);
        clientRepositoryMock.Setup(r => r.GetByIDAsync(clientID, true)).ReturnsAsync(existingClinet);
        clientAuthorizationServiceMock.Setup(a => a.Authorize(ResourceOperation.Update, existingClinet)).Returns(false);

        Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<ForbidException>();//.WithMessage($"Forbidden.");
    }

}