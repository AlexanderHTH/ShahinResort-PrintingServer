using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IDepartureListItemRepository : IRepository<DepartureListItem>
{
    Task<DepartureListItem?> GetByIDAsync(Guid id);
}
