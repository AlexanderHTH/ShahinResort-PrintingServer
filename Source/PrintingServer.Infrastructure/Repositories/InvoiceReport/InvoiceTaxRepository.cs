using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class InvoiceTaxRepository(Print_DBContext dbContext) : RepositoryBase<InvoiceTax>(dbContext), IInvoiceTaxRepository
{
    public async Task<InvoiceTax?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.InvoiceTaxs.FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}
