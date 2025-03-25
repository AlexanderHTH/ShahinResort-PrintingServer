using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class ReservationItemRepository(Print_DBContext dbContext) : RepositoryBase<ReservationItem>(dbContext), IReservationItemRepository
{
    public async Task<ReservationItem?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.ReservationItems.FirstOrDefaultAsync(p=>p.Id== id);
        return toreturn;
    }
}
