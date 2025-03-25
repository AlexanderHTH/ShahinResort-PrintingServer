using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Exceptions;

namespace PrintingServer.Application.UsersManagment.Comands
{
    public class ResendConfirmEmailCommand : IRequest
    {
        public string Email { get; set; } = default!;
    }
    public class ResendConfirmEmailCommandHandler(ITQLogger<ResendConfirmEmailCommandHandler> logger,
                                                  UserManager<AppUser> userManager,
                                                  //IEmailService emailService,
                                                  IConfiguration configuration) : IRequestHandler<ResendConfirmEmailCommand>
    {
        public async Task Handle(ResendConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email) ?? throw new NotFoundException(nameof(AppUser), request.Email);
            logger.LogInformation("Resending confirm email to ({email}).", request.Email);
            if (await userManager.IsEmailConfirmedAsync(user))
            {
                throw new Exception($"Email ({request.Email}) already confirmed.");
            }
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{configuration["AppSettings:FrontendUrl"]}/confirm-email?userId={user.Id}&token={WebUtility.UrlEncode(token)}";
            //await emailService.SendAsync(user.Email, "Confirm Your Email", $"Click here: {confirmationLink}");

        }
    }
}
