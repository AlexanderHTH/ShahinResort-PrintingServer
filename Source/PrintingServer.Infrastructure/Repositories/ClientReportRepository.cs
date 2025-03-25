using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class ClientReportRepository(Print_DBContext dbContext) : RepositoryBase<ClientReport>(dbContext), IClientReportRepository
{
    public async Task<IEnumerable<ClientReport>> ActiveAsync(CancellationToken cancellationToken)
    {
        var toreturn = await _dbContext.ClientReports.Where(p => p.IsActive).ToListAsync(cancellationToken);
        return toreturn;
    }

    public async Task<ClientReport?> FindAsync(ClientReport clientReport)
    {
        var toreturn = await _dbContext.ClientReports.FirstOrDefaultAsync(p => p.ClientId == clientReport.ClientId && p.PrinterId == clientReport.PrinterId && p.ReportId == clientReport.ReportId);
        return toreturn;
    }

    public async Task<ClientReport?> GetByIDAsync(Guid id, bool withsubdata = true)
    {
        var toreturn = await _dbContext.ClientReports.Include(cr => cr.Printeds).FirstOrDefaultAsync(p => p.Id == id);
        if (toreturn != null && !withsubdata)
        {
            toreturn.Printeds = null;
        }
        return toreturn;
    }
}