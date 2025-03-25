using Microsoft.Extensions.Logging;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;

namespace PrintingServer.Infrastructure.Authorization.Services;

public class ClientReportAuthorizationService(ITQLogger<ClientReportAuthorizationService> logger,
                                              IUserContext userContext) : IEntityAuthorizationService<ClientReport>
{
    public bool Authorize(ResourceOperation operation, ClientReport? entity = null)
    {
        var user = userContext.GetCurrentUser();
        if (user != null)
        {
            logger.LogInformation("Authorizing user {UserName}, to {Operation} for client-report {Name}", user.Email, operation, entity == null ? "" : entity.Id.ToString());
            if (operation == ResourceOperation.Read || operation == ResourceOperation.Create)
            {
                logger.LogInformation("Create/Read operation - Successfully authorized.");
                return true;
            }
            if (operation == ResourceOperation.Delete && user.IsInRole(UserRoles.Manager))
            {
                logger.LogInformation("Manager User - Delete operation - Successfully authorized.");
                return true;
            }
            if (entity != null && (operation == ResourceOperation.Update || operation == ResourceOperation.Delete) && user.Id == entity.AppUserId)
            {
                logger.LogInformation("Owner User - Update/Delete operation - Successfully authorized.");
                return true;
            }
        }
        logger.LogWarning("No user logged in.");
        return false;
    }
}
