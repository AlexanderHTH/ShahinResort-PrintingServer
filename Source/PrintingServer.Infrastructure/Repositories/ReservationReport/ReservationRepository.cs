using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class ReservationRepository(Print_DBContext dbContext) : RepositoryBase<Reservation>(dbContext), IReservationRepository
{
    public async Task<Reservation?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.Reservations.Include(p=>p.Items).FirstOrDefaultAsync(o=>o.Id == id);
        return toreturn;
    }
}
