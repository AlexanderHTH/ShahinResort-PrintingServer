using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintingServer.Application.UsersManagment;
using PrintingServer.Application.UsersManagment.Comands;
using PrintingServer.Application.UsersManagment.Dtos;
using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Exceptions;

namespace PrintingServer.API.Controllers
{
    [ApiController]
    [Route("api/UserManagment")]
    public class IdentityController(IMediator mediator,
                                    IUserContext userContext) : ControllerBase
    {
        /// <summary>
        /// Update last login IP.
        /// </summary>
        /// <param name="command"></param>
        [HttpPost("UpdateIP")]
        public async Task UpdateUserIPAddress(UpdateAppUserIPAddress command) => await mediator.Send(command);
        /// <summary>
        /// Add role.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Created role GUID.</returns>
        [HttpPost("AddRole")]
        [Authorize(Roles = UserRoles.Manager)]
        public async Task<Guid> CreateNewRole(CreateRoleCommand command) => await mediator.Send(command);
        /// <summary>
        /// Assign Role to User.
        /// </summary>
        /// <param name="assign">Contains {RoleName, UserEmail}</param>
        [HttpPost("AssignRole")]
        [Authorize(Roles = UserRoles.Manager)]
        public async Task AssignRole(AssignUserRole assign) => await mediator.Send(assign);
        /// <summary>
        /// Unassign a Role from User.
        /// </summary>
        /// <param name="assign">Contains {RoleName, UserEmail}.</param>
        [HttpDelete("UnAssignRole")]
        [Authorize(Roles = UserRoles.Manager)]
        public async Task UnAssignRole(UnAssignUserRole assign) => await mediator.Send(assign);
        /// <summary>
        /// Register new User.
        /// </summary>
        /// <param name="command">RegisterDTO {UserName, UserEmail, Password}</param>
        [HttpPatch("Register")]
        [AllowAnonymous]
        public async Task Register(RegisterUserCommand command) => await mediator.Send(command);
        /// <summary>
        /// User login.
        /// </summary>
        /// <param name="command">{UserName, Password}</param>
        /// <returns></returns>
        [HttpPatch("Login")]
        [AllowAnonymous]
        public async Task<TokenDTO> Login(UserLoginCommand command) => await mediator.Send(command);
        /// <summary>
        /// Logout current user.
        /// </summary>
        [HttpPost("Logout")]
        [AllowAnonymous]
        public async Task Logout(UserLogoutCommand command)
        {
            await mediator.Send(command);
        }
        /// <summary>
        /// Refresh user token.
        /// </summary>
        /// <param name="command">RefreshToken value of the user.</param>
        /// <returns>TokenDTO with new Token value and save a new refresh token.</returns>
        [HttpPost("Refresh_Token")]
        [Authorize]
        public async Task<TokenDTO> RefreshToken(RefreshTokenCommand command) => await mediator.Send(command);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="NotMatchedException"></exception>
        [HttpPost("Confirm_Email")]
        [Authorize]
        public async Task ConfirmEmail(ConfirmEmailCommand command)
        {
            var id = command.UserId;
            var uid = userContext.GetCurrentUser()!.Id;
            if (uid == id)
                await mediator.Send(command);
            else
                throw new NotMatchedException(nameof(AppUser), id.ToString(), uid.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="NotMatchedException"></exception>
        [HttpPost("Forget_Password")]
        [Authorize]
        public async Task ForgetPassword(ForgotPasswordCommand command)
        {
            var email = command.Email.ToUpper();
            var uemail = userContext.GetCurrentUser()!.Email.ToUpper();
            if (email == uemail)
                await mediator.Send(command);
            else
                throw new NotMatchedException(nameof(AppUser), email, uemail);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="NotMatchedException"></exception>
        [HttpPost("Reset_Password")]
        [Authorize]
        public async Task ResetPassword(ResetPasswordCommand command)
        {
            var email = command.Email.ToUpper();
            var uemail = userContext.GetCurrentUser()!.Email.ToUpper();
            if (email == uemail)
                await mediator.Send(command);
            else
                throw new NotMatchedException(nameof(AppUser), email, uemail);
        }
    }

}
