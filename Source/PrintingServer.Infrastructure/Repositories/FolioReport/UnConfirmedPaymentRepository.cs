using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class UnConfirmedPaymentRepository(Print_DBContext dbContext) : RepositoryBase<UnConfirmedPayment>(dbContext), IUnConfirmedPaymentRepository
{
    public async Task<UnConfirmedPayment?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.UnConfirmedPayments.FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}
