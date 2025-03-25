using Microsoft.AspNetCore.Identity;

namespace PrintingServer.Domain.Entities.UserEntities;

public class AppRole : IdentityRole<Guid>
{
    public AppRole()
    {

    }
    //public AppRole(string roleName)
    //{
    //    Name = roleName;
    //    NormalizedName = roleName.ToUpper();
    //    _ = new IdentityRole(roleName);
    //}
}
