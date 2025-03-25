using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class AccoumodationListRepository(Print_DBContext dbContext) : RepositoryBase<AccoumodationList>(dbContext), IAccoumodationListRepository
{
    public async Task<AccoumodationList?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.AccoumodationLists.Include(p => p.TheList).FirstOrDefaultAsync(o => o.Id == id);
        return toreturn;
    }
}
