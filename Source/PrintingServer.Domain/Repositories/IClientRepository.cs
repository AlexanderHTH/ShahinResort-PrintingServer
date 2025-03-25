using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;

public interface IClientRepository : IRepository<Client>
{
    Task<Client?> GetByIDAsync(Guid id, bool withsubdata = true);
    Task<Client?> GetByClientNameAsync(string clientname, bool withsubdata = true);
    Task<Client?> GetByClientIPAsync(string clientip, bool withsubdata = true);
    Task<bool> FindAsync(Client client);
    Task<IEnumerable<Client>> ActiveAsync(CancellationToken cancellationToken);
}
