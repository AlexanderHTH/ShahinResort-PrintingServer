using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class CheckOutListRepository(Print_DBContext dbContext) : RepositoryBase<CheckOutList>(dbContext), ICheckOutListRepository
{
    public async Task<CheckOutList?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.CheckOutLists.Include(p => p.TheList).FirstOrDefaultAsync(x => x.Id == id);
        return toreturn;
    }
}
