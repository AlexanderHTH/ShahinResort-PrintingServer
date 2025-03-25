using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IAccoumodationItemRepository: IRepository<AccoumodationItem>
{
    Task<AccoumodationItem?> GetByIDAsync(Guid id);
}
