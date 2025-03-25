using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class InvoiceRepository(Print_DBContext dbContext) : RepositoryBase<Invoice>(dbContext), IInvoiceRepository
{
    public async Task<Invoice?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.Invoices.Include(p => p.Items).ThenInclude(p1 => p1.invoiceItemTaxes)
                                               .Include(p => p.Taxs)
                                               .FirstOrDefaultAsync(o => o.Id == id);
        return toreturn;
    }
}
