using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class VoucherRepository(Print_DBContext dbContext) : RepositoryBase<Voucher>(dbContext), IVoucherRepository
{
    public async Task<Voucher?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.Vouchers.Include(p => p.Vouchers).FirstOrDefaultAsync(o => o.Id == id);
        return toreturn;
    }
}
