using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class InvoiceItemTaxRepository(Print_DBContext dbContext) : RepositoryBase<InvoiceItemTax>(dbContext), IInvoiceItemTaxRepository
{
    public async Task<InvoiceItemTax?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.InvoiceItemTaxes.FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}
