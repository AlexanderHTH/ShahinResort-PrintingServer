using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class InvoicesListItemRepository(Print_DBContext dbContext) : RepositoryBase<InvoicesListItem>(dbContext), IInvoicesListItemRepository
{
    public async Task<InvoicesListItem?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.InvoicesListItems.FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}
