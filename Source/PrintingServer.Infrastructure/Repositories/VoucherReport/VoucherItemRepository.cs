using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class VoucherItemRepository(Print_DBContext dbContext) : RepositoryBase<VoucherItem>(dbContext), IVoucherItemRepository
{
    public async Task<VoucherItem?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.VoucherItems.FirstOrDefaultAsync(o => o.Id == id);
        return toreturn;
    }
}