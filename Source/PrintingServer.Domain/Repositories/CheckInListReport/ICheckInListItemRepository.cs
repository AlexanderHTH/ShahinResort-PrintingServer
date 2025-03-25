using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface ICheckInListItemRepository : IRepository<CheckInListItem>
{
    Task<CheckInListItem?> GetByIDAsync(Guid id);
}
