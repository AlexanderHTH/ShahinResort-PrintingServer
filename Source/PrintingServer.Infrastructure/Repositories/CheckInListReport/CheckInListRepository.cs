using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class CheckInListRepository(Print_DBContext dbContext) : RepositoryBase<CheckInList>(dbContext), ICheckInListRepository
{
      public async Task<CheckInList?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.CheckInLists.Include(p => p.TheList).FirstOrDefaultAsync(x => x.Id == id);
        return toreturn;
    }
}
