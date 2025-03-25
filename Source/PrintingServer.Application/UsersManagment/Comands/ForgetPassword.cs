using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Exceptions;

namespace PrintingServer.Application.UsersManagment.Comands;
public class ForgotPasswordCommand : IRequest
{
    public string Email { get; set; } = default!;
}

public class ForgotPasswordCommandHandler(ITQLogger<ForgotPasswordCommandHandler> logger,
                                          UserManager<AppUser> userManager,
                                          //IEmailService emailService,
                                          //IConfiguration configuration,
                                          IHttpContextAccessor httpContextAccessor) : IRequestHandler<ForgotPasswordCommand>
{
    public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email) ?? throw new NotFoundException(nameof(AppUser), request.Email);
        logger.LogInformation($"Sending forget password request for ({request.Email})");
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        //var resetLink = $"{configuration["AppSettings:FrontendUrl"]}/reset-password?email={user.Email}&token={WebUtility.UrlEncode(token)}";
        var resetLink = $"{httpContextAccessor?.HttpContext?.Request.Host}/reset-password?email={user.Email}&token={WebUtility.UrlEncode(token)}";
        //await emailService.SendAsync(user.Email, "Reset Your Password", $"Click here: {resetLink}");
    }
}
