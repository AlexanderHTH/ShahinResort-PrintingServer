using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Exceptions;

namespace PrintingServer.Application.UsersManagment.Comands;

public class UserLogoutCommand : IRequest
{
}
public class UserLogoutCommandHandler(ITQLogger<UserLoginCommandHandler> logger,
                                      IUserContext userContext,
                                      SignInManager<AppUser> signInManager,
                                      UserManager<AppUser> userManager) : IRequestHandler<UserLogoutCommand>
{
    public async Task Handle(UserLogoutCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var currentUser = userContext.GetCurrentUser()!;
            var user = await userManager.FindByIdAsync(currentUser.Id.ToString());
            if (user == null)
            {
                throw new NotFoundException(nameof(AppUser), currentUser.Id.ToString());
            }
            else
            {
                logger.LogInformation("User {UserName} loging out", user.Id.ToString());
                user.TokenVersion++; // Increment token version
                await userManager.UpdateAsync(user);
                await signInManager.SignOutAsync();
            }
        }
        catch
        {
            throw new HandlingDataException(nameof(AppUser), "Failed to logout.");
        }
    }
}
