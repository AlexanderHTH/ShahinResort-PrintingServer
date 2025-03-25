using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Exceptions;

namespace PrintingServer.Application.UsersManagment.Comands;

public class UpdateAppUserIPAddress : IRequest
{
    public string? IPAddress { get; set; }
}
public class UpdateAppUserIPAddressHandler(ITQLogger<UpdateAppUserIPAddressHandler> logger,
                                           IUserContext userContext,
                                           IUserStore<AppUser> userStore) : IRequestHandler<UpdateAppUserIPAddress>
{
    public async Task Handle(UpdateAppUserIPAddress request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Set user ({UserID}) IP address {@Request}", user!.Id.ToString(), request);
        var dbUser = await userStore.FindByIdAsync(user!.Id.ToString(), cancellationToken) ?? throw new NotFoundException(nameof(AppUser), user!.Id.ToString());
        dbUser.LastUsedIPAddress = request.IPAddress;
        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}

