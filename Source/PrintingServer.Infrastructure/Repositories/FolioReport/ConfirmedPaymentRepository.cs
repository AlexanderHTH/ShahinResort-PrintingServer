using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories
{
    internal class ConfirmedPaymentRepository(Print_DBContext dbContext) : RepositoryBase<ConfirmedPayment>(dbContext), IConfirmedPaymentRepository
    {
        public async Task<ConfirmedPayment?> GetByIDAsync(Guid id)
        {
            var toreturn = await _dbContext.ConfirmedPayments.FirstOrDefaultAsync(p => p.Id == id);
            return toreturn;
        }
    }
}
