using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PrintingServer.Domain.Entities.UserEntities;

namespace PrintingServer.Infrastructure.Authorization;

public class AppUserClaimsPrincipalFactory(UserManager<AppUser> userManager, 
                                         RoleManager<AppRole> roleManager, 
                                         IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<AppUser, AppRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(AppUser user)
    {
        var id = await GenerateClaimsAsync(user);
        //
        // CREATE ANY NEW CLAIMS PRINCIPAL FOR USER
        //
        if (user.UserName != null)
        {
            id.AddClaim(new Claim(Policy.UserName, user.UserName));
        }
        //
        return new ClaimsPrincipal(id);
    }
}
