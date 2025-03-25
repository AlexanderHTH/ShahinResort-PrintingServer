using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Exceptions;

namespace PrintingServer.Application.UsersManagment.Comands;

public class ConfirmEmailCommand : IRequest
{
    public Guid UserId { get; set; } = default!;
    public string Token { get; set; } = default!;
}

public class ConfirmEmailCommandHandler(ITQLogger<ConfirmEmailCommandHandler> logger,
                                        UserManager<AppUser> userManager) : IRequestHandler<ConfirmEmailCommand>
{
    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            throw new NotFoundException(nameof(AppUser), request.UserId.ToString());
        logger.LogInformation("Confirm email ({Email}) for user.", user.Email!);
        var result = await userManager.ConfirmEmailAsync(user, request.Token);
        if (!result.Succeeded)
            throw new Exception("Email confirmation failed.");
    }
}
