using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class RequiredItemRepository(Print_DBContext dbContext) : RepositoryBase<RequiredItem>(dbContext), IRequiredItemRepository
{
    public async Task<RequiredItem?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.RequiredItems.FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}
