using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class ClientRepository(Print_DBContext dbContext) : RepositoryBase<Client>(dbContext), IClientRepository
{
    public async Task<IEnumerable<Client>> ActiveAsync(CancellationToken cancellationToken)
    {
        var toreturn = await _dbContext.Clients.Where(c => c.IsActive).ToListAsync(cancellationToken);
        return toreturn;
    }

    public async Task<bool> FindAsync(Client client)
    {
        var find = await _dbContext.Clients.FirstOrDefaultAsync(o => o.ClientName == client.ClientName || o.ClientIp == client.ClientIp);
        return find != null;
    }

    public async Task<Client?> GetByClientIPAsync(string clientip, bool withsubdata = true)
    {
        var toreturn = await _dbContext.Clients.Include(p => p.ClientReports).FirstOrDefaultAsync(o => o.ClientIp == clientip);
        if (toreturn != null && !withsubdata)
        {
            toreturn.ClientReports = null;
        }
        return toreturn;
    }

    public async Task<Client?> GetByClientNameAsync(string clientname, bool withsubdata = true)
    {
        var toreturn = await _dbContext.Clients.Include(p => p.ClientReports).FirstOrDefaultAsync(o => o.ClientName == clientname);
        if (toreturn != null && !withsubdata)
        {
            toreturn.ClientReports = null;
        }
        return toreturn;
    }

    public async Task<Client?> GetByIDAsync(Guid id, bool withsubdata = true)
    {
        var toreturn = await _dbContext.Clients.Include(p => p.ClientReports).FirstOrDefaultAsync(o => o.Id == id);
        if (toreturn != null && !withsubdata)
        {
            toreturn.ClientReports = null;
        }
        return toreturn;
    }

}
