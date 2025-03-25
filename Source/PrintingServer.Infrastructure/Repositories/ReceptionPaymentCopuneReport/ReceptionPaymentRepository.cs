using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class ReceptionPaymentRepository(Print_DBContext dbContext) : RepositoryBase<ReceptionPayment>(dbContext), IReceptionPaymentRepository
{
    public async Task<ReceptionPayment?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.ReceptionPayments.FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}
