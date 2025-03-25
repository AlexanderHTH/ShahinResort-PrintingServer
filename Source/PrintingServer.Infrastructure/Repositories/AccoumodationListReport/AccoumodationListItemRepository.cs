using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class AccoumodationListItemRepository(Print_DBContext dbContext) : RepositoryBase<AccoumodationListItem>(print_DBContext: dbContext), IAccoumodationListItemRepository
{
    public async Task<AccoumodationListItem?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.AccoumodationListItems.FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}
