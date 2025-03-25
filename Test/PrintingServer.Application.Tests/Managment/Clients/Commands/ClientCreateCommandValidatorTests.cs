using FluentValidation.TestHelper;
using Xunit;

namespace PrintingServer.Application.Managment.Clients.Commands.Tests;

public class ClientCreateCommandValidatorTests
{
    [Fact()]
    public void ClientCreateCommandValidatorTest_Validator_OK()
    {
        // Arrange
        var command = new ClientCreateCommand()
        {
            ClientName = "TEST",
            ClientIp = "120.10.10.10"
        };
        var validator = new ClientCreateCommandValidator();
        // Act
        var result = validator.TestValidate(command);
        // Arrest
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact()]
    public void ClientCreateCommandValidatorTest_Validator_Bad()
    {
        // Arrange
        var command = new ClientCreateCommand()
        {
            ClientName = "TT",
            ClientIp = "120.10.10.666"
        };
        var validator = new ClientCreateCommandValidator();
        // Act
        var result = validator.TestValidate(command);
        // Arrest
        result.ShouldHaveValidationErrorFor(co => co.ClientName);
        result.ShouldHaveValidationErrorFor(co => co.ClientIp);
    }
}