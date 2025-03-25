using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IAccoumodationListItemRepository : IRepository<AccoumodationListItem>
{
    Task<AccoumodationListItem?> GetByIDAsync(Guid id);
}
