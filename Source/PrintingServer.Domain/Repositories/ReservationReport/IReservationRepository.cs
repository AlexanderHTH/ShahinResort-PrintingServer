using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IReservationRepository: IRepository<Reservation>
{
    Task<Reservation?> GetByIDAsync(Guid id);
}
