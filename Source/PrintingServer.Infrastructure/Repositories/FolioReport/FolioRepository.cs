using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class FolioRepository(Print_DBContext dbContext) : RepositoryBase<Folio>(dbContext), IFolioRepository
{
    public async Task<Folio?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.Folios.Include(p => p.AllRegistration)
                                             .Include(p => p.ConfirmedPayment)
                                             .Include(p => p.UnconfirmedPayment)
                                             .Include(p => p.ConfirmedINS)
                                             .Include(p => p.UnconfirmedINS)
                                             .Include(p => p.Transfare)
                                             .Include(p => p.AllRequired)
                                             .FirstOrDefaultAsync(o => o.Id == id);
        return toreturn;
    }
}
