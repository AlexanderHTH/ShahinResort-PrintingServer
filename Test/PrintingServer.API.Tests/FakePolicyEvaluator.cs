using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Moq;
using PrintingServer.Application.UsersManagment;

namespace PrintingServer.API.Tests;

internal class FakePolicyEvaluator : IPolicyEvaluator
{
    public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        var claimsP = new ClaimsPrincipal();
        claimsP.AddIdentity(new ClaimsIdentity(
            new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role,"Manager")
            }));
        var ticket = new AuthenticationTicket(claimsP, "TEST");
        var result = AuthenticateResult.Success(ticket);
        var userContextMock = new Mock<IUserContext>();
        var userID = Guid.NewGuid();
        var currentUser = new CurrentUser(userID, "test", "test@test.com", []);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);
        return Task.FromResult(result);
    }

    public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
    {
        return Task.FromResult(PolicyAuthorizationResult.Success());
    }
}
