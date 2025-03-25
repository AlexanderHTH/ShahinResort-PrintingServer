using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface ITransfareRepository : IRepository<Transfare>
{
    Task<Transfare?> GetByIDAsync(Guid id);
}
