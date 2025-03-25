using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class CashPaymentRepository(Print_DBContext dbContext) : RepositoryBase<CashPayment>(dbContext), ICashPaymentRepository
{
    public async Task<CashPayment?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.CashPayments.FirstOrDefaultAsync(o => o.Id == id);
        return toreturn;
    }
}
