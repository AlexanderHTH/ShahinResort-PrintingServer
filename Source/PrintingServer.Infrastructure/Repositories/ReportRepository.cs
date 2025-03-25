using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class ReportRepository(Print_DBContext dbContext) : RepositoryBase<Report>(dbContext), IReportRepository
{
    public async Task<IEnumerable<Report>> ActiveAsync(CancellationToken cancellationToken)
    {
        var toreturn = await _dbContext.Reports.Where(r => r.IsActive).ToListAsync(cancellationToken);
        return toreturn;
    }

    public async Task<bool> FindAsync(Report report)
    {
        var toreturn = await _dbContext.Reports.FirstOrDefaultAsync(r => r.ReportName == report.Name);
        return toreturn != null;
    }

    public async Task<Report?> GetByIDAsync(Guid id, bool withsubdata = true)
    {
        var toreturn=await _dbContext.Reports.Include(p => p.ClientReports).FirstOrDefaultAsync(o => o.Id == id);
        if (toreturn != null && !withsubdata)
        {
            toreturn.ClientReports = null;
        }
        return toreturn;
    }
}
