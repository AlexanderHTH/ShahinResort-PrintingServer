using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface ICheckInListRepository: IRepository<CheckInList>
{
    Task<CheckInList?> GetByIDAsync(Guid id);
}
