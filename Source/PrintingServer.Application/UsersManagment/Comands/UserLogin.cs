using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.UsersManagment.Dtos;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Exceptions;
using PrintingServer.Domain.Tokens;

namespace PrintingServer.Application.UsersManagment.Comands
{
    public class UserLoginCommand : IRequest<TokenDTO>
    {
        public LoginDTO LoginDTO { get; set; } = default!;
    }
    public class UserLoginCommandHandler(ITQLogger<UserLoginCommandHandler> logger,
                                         UserManager<AppUser> userManager,
                                         SignInManager<AppUser> signInManager,
                                         IJwtTokenService jwtTokenService,
                                         IHttpContextAccessor httpContextAccessor) : IRequestHandler<UserLoginCommand, TokenDTO>
    {
        public async Task<TokenDTO> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"User ({request.LoginDTO.Username}) trying to login. {httpContextAccessor?.HttpContext?.Request.Host}");
            var user = await userManager.FindByNameAsync(request.LoginDTO.Username) ??
                      (await userManager.FindByEmailAsync(request.LoginDTO.Username) ??
                      throw new NotFoundException(nameof(AppUser), string.Format("User ({0}) not found.", request.LoginDTO.Username)));

            var result = await signInManager.CheckPasswordSignInAsync(user, request.LoginDTO.Password, false);
            if (!result.Succeeded)
                throw new HandlingDataException(nameof(AppUser), "Invalid password.");

            var token = await jwtTokenService.GenerateToken(user);
            return new TokenDTO
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                ExpiresIn = token.ExpiresIn,
                TokenType = token.TokenType
            };
        }
    }
}
