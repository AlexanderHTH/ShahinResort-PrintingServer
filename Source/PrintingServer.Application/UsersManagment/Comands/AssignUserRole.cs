using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Exceptions;

namespace PrintingServer.Application.UsersManagment.Comands;

/// <summary>
/// Assign a role(RoleName) to user(UserEmail).
/// </summary>
public class AssignUserRole : IRequest
{
    public string UserName { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}
public class AssignUserRoleHandler(ITQLogger<AssignUserRoleHandler> logger,
                                   UserManager<AppUser> userManager,
                                   RoleManager<AppRole> roleManager) : IRequestHandler<AssignUserRole>
{
    public async Task Handle(AssignUserRole request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assigning user role: {@Request}", request);
        var user = await userManager.FindByEmailAsync(request.UserName) ?? throw new NotFoundException(nameof(AppUser), request.UserName);
        var role = await roleManager.FindByNameAsync(request.RoleName) ?? throw new NotFoundException(nameof(AppRole), request.RoleName);
        await userManager.AddToRoleAsync(user, role.Name!);
    }
}
