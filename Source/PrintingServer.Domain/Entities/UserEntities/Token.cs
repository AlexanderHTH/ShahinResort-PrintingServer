namespace PrintingServer.Domain.Entities.UserEntities;

public class Token
{
    public string TokenType { get; set; } = "Bearer";
    public string AccessToken { get; set; } = default!;
    public DateTime ExpiresIn { get; set; }
    public string RefreshToken { get; set; } = default!;
}
