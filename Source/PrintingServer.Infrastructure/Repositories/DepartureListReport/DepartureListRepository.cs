using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class DepartureListRepository(Print_DBContext dbContext) : RepositoryBase<DepartureList>(dbContext), IDepartureListRepository
{
    public async Task<DepartureList?> GetByIDAsync(Guid id)
    {
        var toretrurn = await _dbContext.DepartureLists.Include(p=>p.TheList).FirstOrDefaultAsync(o=>o.Id== id);
        return toretrurn;
    }
}
