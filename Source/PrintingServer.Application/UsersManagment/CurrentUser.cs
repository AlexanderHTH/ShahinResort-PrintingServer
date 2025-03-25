namespace PrintingServer.Application.UsersManagment;

public record CurrentUser(Guid Id, string Email, string UserName, IEnumerable<string> Roles)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
