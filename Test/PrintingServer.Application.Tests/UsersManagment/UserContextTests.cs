using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using PrintingServer.Domain.Constants;
using Xunit;

namespace PrintingServer.Application.UsersManagment.Tests;

public class UserContextTests
{
    [Fact()]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        var guid = Guid.NewGuid(); 
        // Arragnge
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var userContext = new UserContext(httpContextAccessorMock.Object);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier,guid.ToString()),
            new Claim(ClaimTypes.Email,"test@test.com"),
            new Claim(ClaimTypes.Name,"TEST"),
            new Claim(ClaimTypes.Role , UserRoles.Manager),
            new Claim(ClaimTypes.Role , UserRoles.Administrator)
        };
        var user = new ClaimsPrincipal(new  ClaimsIdentity(claims, "TEST"));    
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });

        // Act
        var currentUser = userContext.GetCurrentUser();

        // Assert
        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be(guid);
        currentUser.Email.Should().Be("test@test.com");
        currentUser.Roles.Should().ContainInOrder(UserRoles.Manager, UserRoles.Administrator);
    }
}