using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class PrintedRepository(Print_DBContext dbContext) : RepositoryBase<Printed>(dbContext), IPrintedRepository
{
   public async Task<Printed?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.Printeds.FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}
