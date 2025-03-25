using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IFolioRepository : IRepository<Folio>
{
    Task<Folio?> GetByIDAsync(Guid id);
}
