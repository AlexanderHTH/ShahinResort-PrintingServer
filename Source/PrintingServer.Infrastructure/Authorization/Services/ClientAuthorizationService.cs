using Microsoft.Extensions.Logging;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;

namespace PrintingServer.Infrastructure.Authorization.Services;
public class ClientAuthorizationService(ITQLogger<ClientAuthorizationService> logger,
                                        IUserContext userContext) : IEntityAuthorizationService<Client>
{
    public bool Authorize(ResourceOperation operation, Client? client = null)
    {
        var user = userContext.GetCurrentUser();
        if (user != null)
        {
            logger.LogInformation("Authorizing user {UserName}, to {Operation} on client {ClientName}", user.Email, operation, client != null ? client.Id.ToString() : "");
            if (operation == ResourceOperation.Read || operation == ResourceOperation.Create)
            {
                logger.LogInformation("Create/Read operation - Successfully authorized.");
                return true;
            }
            if (user.IsInRole(UserRoles.Manager) && (operation == ResourceOperation.Update || operation == ResourceOperation.Delete))
            {
                logger.LogInformation("Manager User - Update/Delete operation - Successfully authorized.");
                return true;
            }
            if (client != null && (operation == ResourceOperation.Update || operation == ResourceOperation.Delete) && user.Id == client.AppUserId)
            {
                logger.LogInformation("Owner User - Update/Delete operation - Successfully authorized.");
                return true;
            }
        }
        logger.LogWarning("No user logged in.");
        return false;
    }
}
