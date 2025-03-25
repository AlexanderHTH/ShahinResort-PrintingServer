using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class AccoumodationRepository(Print_DBContext dbContext) : RepositoryBase<Accoumodation>(dbContext), IAccoumodationRepository
{
    public async Task<Accoumodation?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.Accoumodations.Include(p=>p.Items).FirstOrDefaultAsync(o=>o.Id == id);
        return toreturn;
    }
}
