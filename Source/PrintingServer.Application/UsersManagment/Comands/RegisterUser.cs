using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.UsersManagment.Dtos;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Exceptions;

namespace PrintingServer.Application.UsersManagment.Comands
{
    public class RegisterUserCommand : IRequest
    {
        public RegisterDTO RegisterDTO { get; set; } = default!;
    }
    public class RegisterUserCommandHandler(ITQLogger<UserLoginCommandHandler> logger,
                                            UserManager<AppUser> userManager,
                                            IPasswordHasher<AppUser> passwordHasher) : IRequestHandler<RegisterUserCommand>
    {
        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating User ({UserName}).", request.RegisterDTO.Username);
            AppUser user = new()
            {
                Id = Guid.NewGuid(),
                UserName = request.RegisterDTO.Username,
                Email = request.RegisterDTO.Email,
                NormalizedUserName = request.RegisterDTO.Username.ToUpper(),
                NormalizedEmail = request.RegisterDTO.Email.ToUpper(),
                LockoutEnabled = false
            };
            var hashedPassword = passwordHasher.HashPassword(user, AccountSettings.DefaultPassword);
            user.SecurityStamp = user.Id.ToString();
            user.PasswordHash = hashedPassword;

            var result = await userManager.CreateAsync(user);//, request.registerDTO.Password);
            if (!result.Succeeded)
            {
                throw new HandlingDataException(nameof(AppUser), "Failed to register user.");
            }
        }
    }
}
