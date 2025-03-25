using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Entities.UserEntities;

namespace PrintingServer.Application.UsersManagment.Comands;

public class CreateRoleCommand : IRequest<Guid>
{
    public string RoleName { get; set; } = default!;
}
public class CreateRoleCommandHandler(ITQLogger<UpdateAppUserIPAddressHandler> logger,
                                      IRoleStore<AppRole> roleStore) : IRequestHandler<CreateRoleCommand, Guid>
{
    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new AppRole()
        {
            Name = request.RoleName,
            NormalizedName = request.RoleName.ToUpper(),
            Id = Guid.NewGuid()
        };
        logger.LogInformation("Creating new role {rolename}.", request.RoleName);
        await roleStore.CreateAsync(role,cancellationToken);
        return (await roleStore.FindByNameAsync(request.RoleName!, cancellationToken))!.Id;
    }
}