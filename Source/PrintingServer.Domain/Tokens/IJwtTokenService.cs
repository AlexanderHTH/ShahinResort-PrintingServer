using PrintingServer.Domain.Entities.UserEntities;

namespace PrintingServer.Domain.Tokens;

public interface IJwtTokenService
{
    Task<Token> GenerateToken(AppUser user, int tokenduration_InMinutes = 120);
}
