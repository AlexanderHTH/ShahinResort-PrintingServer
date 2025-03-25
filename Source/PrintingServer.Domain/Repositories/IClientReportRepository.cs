using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;

public interface IClientReportRepository : IRepository<ClientReport> 
{
    Task<ClientReport?> GetByIDAsync(Guid id, bool withsubdata = true);
    Task<ClientReport?> FindAsync(ClientReport clientReport);
    Task<IEnumerable<ClientReport>> ActiveAsync(CancellationToken cancellationToken);
}
