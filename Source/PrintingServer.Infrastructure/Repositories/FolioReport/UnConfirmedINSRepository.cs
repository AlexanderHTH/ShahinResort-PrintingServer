using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories
{
    internal class UnConfirmedINSRepository(Print_DBContext dbContext) : RepositoryBase<UnConfirmedINS>(dbContext), IUnConfirmedINSRepository
    {
        public async Task<UnConfirmedINS?> GetByIDAsync(Guid id)
        {
            var toreturn = await _dbContext.UnConfirmedINSs.FirstOrDefaultAsync(p => p.Id == id);
            return toreturn;
        }
    }
}
