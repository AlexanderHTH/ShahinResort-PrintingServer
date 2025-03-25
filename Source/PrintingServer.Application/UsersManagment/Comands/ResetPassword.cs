using MediatR;
using Microsoft.AspNetCore.Identity;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Exceptions;

namespace PrintingServer.Application.UsersManagment.Comands;
public class ResetPasswordCommand : IRequest
{
    public string Email { get; set; } = default!;
    public string Token { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}

public class ResetPasswordCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<ResetPasswordCommand>
{
    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email) ?? throw new NotFoundException(nameof(AppUser), request.Email);
        var result = await userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
        if (!result.Succeeded)
            throw new Exception("Password reset failed.");
    }
}
