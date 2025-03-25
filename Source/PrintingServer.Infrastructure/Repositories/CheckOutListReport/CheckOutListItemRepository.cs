using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class CheckOutListItemRepository(Print_DBContext dbContext) : RepositoryBase<CheckOutListItem>(dbContext), ICheckOutListItemRepository
{
    public async Task<CheckOutListItem?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.CheckOutListItems.FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}
