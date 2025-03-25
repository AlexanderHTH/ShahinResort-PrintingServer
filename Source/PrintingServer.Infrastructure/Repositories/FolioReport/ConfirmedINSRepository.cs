using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class ConfirmedINSRepository(Print_DBContext dbContext) : RepositoryBase<ConfirmedINS>(dbContext), IConfirmedINSRepository
{
    public async Task<ConfirmedINS?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.ConfirmedINSs.FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}
