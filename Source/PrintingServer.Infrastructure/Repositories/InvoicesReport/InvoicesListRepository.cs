using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class InvoicesListRepository(Print_DBContext dbContext) : RepositoryBase<InvoicesList>(dbContext), IInvoicesListRepository
{
    public async Task<InvoicesList?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.InvoicesLists.Include(p => p.Items).FirstOrDefaultAsync(o => o.Id == id);
        return toreturn;
    }
}
