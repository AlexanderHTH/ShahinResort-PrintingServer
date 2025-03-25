using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IRequiredItemRepository : IRepository<RequiredItem>
{
    Task<RequiredItem?> GetByIDAsync(Guid id);
}
