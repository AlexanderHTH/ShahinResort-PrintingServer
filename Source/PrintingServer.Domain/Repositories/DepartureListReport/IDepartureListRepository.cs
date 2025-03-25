using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IDepartureListRepository: IRepository<DepartureList>
{
    Task<DepartureList?> GetByIDAsync(Guid id);
}
