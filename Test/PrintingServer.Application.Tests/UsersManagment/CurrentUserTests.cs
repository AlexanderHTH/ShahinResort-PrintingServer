using FluentAssertions;
using PrintingServer.Domain.Constants;
using Xunit;

namespace PrintingServer.Application.UsersManagment.Tests;

public class CurrentUserTests
{
    [Theory()]
    [InlineData(UserRoles.Manager)]
    [InlineData(UserRoles.Administrator)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        // Arrange
        var currentUser = new CurrentUser(Guid.NewGuid(), "TEST", "test@test.com", [UserRoles.Manager, UserRoles.Administrator]);
        // Act
        var isInRole = currentUser.IsInRole(roleName);
        // Assert
        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        // Arrange
        var currentUser = new CurrentUser(Guid.NewGuid(), "TEST", "test@test.com", [UserRoles.Manager, UserRoles.Administrator]);
        // Act
        var isInRole = currentUser.IsInRole(UserRoles.Printer);
        // Assert
        isInRole.Should().BeFalse();
    }

    [Fact()]
    public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
    {
        // Arrange
        var currentUser = new CurrentUser(Guid.NewGuid(), "TEST", "test@test.com", [UserRoles.Manager, UserRoles.Administrator]);
        // Act
        var isInRole = currentUser.IsInRole(UserRoles.Administrator.ToLower());
        // Assert
        isInRole.Should().BeFalse();
    }
}