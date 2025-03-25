using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IExtendedArrivalListRepository: IRepository<ExtendedArrivalList>
{
    Task<ExtendedArrivalList?> GetByIDAsync(Guid id);
}
