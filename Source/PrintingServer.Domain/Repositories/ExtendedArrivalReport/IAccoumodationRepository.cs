using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IAccoumodationRepository: IRepository<Accoumodation>
{
    Task<Accoumodation?> GetByIDAsync(Guid id);
}
