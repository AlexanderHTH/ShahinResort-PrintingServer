using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface ICheckOutListRepository : IRepository<CheckOutList>
{
    Task<CheckOutList?> GetByIDAsync(Guid id);
}
