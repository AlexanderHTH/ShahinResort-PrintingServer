using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrintingServer.Application.UsersManagment.Dtos;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Tokens;
using PrintingServer.Infrastructure.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PrintingServer.Infrastructure.Tokens;

public class JwtTokenService(UserManager<AppUser> _userManager,
                             IConfiguration _config) : IJwtTokenService
{
    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public async Task<Token> GenerateToken(AppUser user, int tokenduration_InMinutes = 120)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email,user.Email!),
            new(Policy.TokenVersion , user.TokenVersion.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpireMinutes"]!));
        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: expires,
            signingCredentials: creds
        );
        return new TokenDTO
        {
            TokenType = "Bearer",
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresIn = expires,
            RefreshToken = GenerateRefreshToken()
        };
    }
}
