using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class RoomNumberRepository(Print_DBContext dbContext) : RepositoryBase<RoomNumber>(dbContext), IRoomNumberRepository
{
    public async Task<RoomNumber?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.RoomNumbers.FirstOrDefaultAsync(o=>o.Id == id);
        return toreturn;
    }
}
