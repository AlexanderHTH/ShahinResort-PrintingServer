using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.UsersManagment.Dtos;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Tokens;

namespace PrintingServer.Application.UsersManagment.Comands;

public class RefreshTokenCommand : IRequest<TokenDTO>
{
    public string RefreshToken { get; set; } = default!;
}

public class RefreshTokenCommandHandler(ITQLogger<RefreshTokenCommandHandler> logger,
                                        UserManager<AppUser> userManager,
                                        IJwtTokenService tokenService) : IRequestHandler<RefreshTokenCommand, TokenDTO>
{
    public async Task<TokenDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken, cancellationToken: cancellationToken);
        if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Invalid or expired refresh token.");
        logger.LogInformation("using RefreshToken for user ({UserName})", user.UserName!);
        var newAccessToken = await tokenService.GenerateToken(user);
        user.RefreshToken = newAccessToken.RefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await userManager.UpdateAsync(user);

        return new TokenDTO()
        {
            TokenType = "Bearer",
            AccessToken = newAccessToken.AccessToken,
            RefreshToken = newAccessToken.RefreshToken,
            ExpiresIn = newAccessToken.ExpiresIn,
        };
    }
}
