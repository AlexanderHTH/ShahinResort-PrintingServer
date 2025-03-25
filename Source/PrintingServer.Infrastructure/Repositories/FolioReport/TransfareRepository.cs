using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories
{
    internal class TransfareRepository(Print_DBContext dbContext) : RepositoryBase<Transfare>(dbContext), ITransfareRepository
    {
        public async Task<Transfare?> GetByIDAsync(Guid id)
        {
            var toreturn = await _dbContext.Transfares.FirstOrDefaultAsync(p => p.Id == id);
            return toreturn;
        }
    }
}
