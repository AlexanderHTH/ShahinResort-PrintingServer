using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;

public interface IRoomNumberRepository: IRepository<RoomNumber>
{
    Task<RoomNumber?> GetByIDAsync(Guid id);
}
