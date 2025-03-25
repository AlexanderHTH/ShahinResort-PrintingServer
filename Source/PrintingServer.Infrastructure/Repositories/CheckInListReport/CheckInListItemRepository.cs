using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class CheckInListItemRepository(Print_DBContext dbContext) : RepositoryBase<CheckInListItem>(dbContext), ICheckInListItemRepository
{
     public async Task<CheckInListItem?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.CheckInListItems.FirstOrDefaultAsync(x => x.Id == id);
        return toreturn;
    }
}
