using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class DepartureListItemRepository(Print_DBContext dbContext) : RepositoryBase<DepartureListItem>(dbContext), IDepartureListItemRepository
{
    public async Task<DepartureListItem?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.DepartureListItems.FirstOrDefaultAsync(p=>p.Id == id);
        return toreturn;
    }
}
