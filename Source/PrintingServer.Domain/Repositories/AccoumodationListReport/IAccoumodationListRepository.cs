using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IAccoumodationListRepository : IRepository<AccoumodationList>
{
    Task<AccoumodationList?> GetByIDAsync(Guid id);
}
