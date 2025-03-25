using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IReservationItemRepository: IRepository<ReservationItem>
{
    Task<ReservationItem?> GetByIDAsync(Guid id);
}
