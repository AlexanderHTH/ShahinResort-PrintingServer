using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class InvoiceItemRepository(Print_DBContext dbContext) : RepositoryBase<InvoiceItem>(dbContext), IInvoiceItemRepository
{
    public async Task<InvoiceItem?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.InvoiceItems.Include(o => o.invoiceItemTaxes).FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}
