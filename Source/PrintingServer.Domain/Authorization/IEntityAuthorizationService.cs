using PrintingServer.Domain.Constants;
using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Authorization;

public interface IEntityAuthorizationService<T> where T : class
{
    bool Authorize(ResourceOperation operation, T? entity = null);
}
