using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class ExtendedArrivalListRepository(Print_DBContext dbContext) : RepositoryBase<ExtendedArrivalList>(dbContext), IExtendedArrivalListRepository
{
    public async Task<ExtendedArrivalList?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.ExtendedArrivalLists.FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}
