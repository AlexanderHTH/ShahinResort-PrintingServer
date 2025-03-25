using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Exceptions;

namespace PrintingServer.Application.UsersManagment.Comands;
/// <summary>
/// Unassign a role(RoleName) from user(UserEmail).
/// </summary>
public class UnAssignUserRole : IRequest
{
    public string UserName { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}
public class UnAssignUserRoleHandler(ITQLogger<UnAssignUserRoleHandler> logger,
                                   UserManager<AppUser> userManager,
                                   RoleManager<AppRole> roleManager) : IRequestHandler<UnAssignUserRole>
{
    public async Task Handle(UnAssignUserRole request, CancellationToken cancellationToken)
    {
        logger.LogInformation("UnAssigning user role: {@Request}", request);
        var user = await userManager.FindByEmailAsync(request.UserName) ?? throw new NotFoundException(nameof(AppUser), request.UserName);
        var role = await roleManager.FindByNameAsync(request.RoleName) ?? throw new NotFoundException(nameof(AppRole), request.RoleName);
        await userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}