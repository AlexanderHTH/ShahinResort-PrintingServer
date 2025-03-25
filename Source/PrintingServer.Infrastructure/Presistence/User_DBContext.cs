using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities.UserEntities;

namespace PrintingServer.Infrastructure.Presistence;

public partial class User_DBContext(DbContextOptions<User_DBContext> options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
{
}
