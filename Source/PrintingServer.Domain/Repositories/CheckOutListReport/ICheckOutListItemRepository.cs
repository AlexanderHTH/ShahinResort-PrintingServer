using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface ICheckOutListItemRepository : IRepository<CheckOutListItem>
{
    Task<CheckOutListItem?> GetByIDAsync(Guid id);
}
