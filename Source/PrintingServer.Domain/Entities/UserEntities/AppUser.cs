using System;
using Microsoft.AspNetCore.Identity;

namespace PrintingServer.Domain.Entities.UserEntities;

public class AppUser : IdentityUser<Guid>
{
    public string? LastUsedIPAddress { get; set; }
    public int TokenVersion { get; set; } = 1;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry {  get; set; }
}
