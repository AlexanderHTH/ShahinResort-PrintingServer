using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class AccoumodationItemRepository(Print_DBContext dbContext) : RepositoryBase<AccoumodationItem>(dbContext), IAccoumodationItemRepository
{
    public async Task<AccoumodationItem?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.AccoumodationItems.FirstOrDefaultAsync(p=> p.Id==id);
        return toreturn;
    }
}
