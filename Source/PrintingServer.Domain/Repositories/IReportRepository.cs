using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;

public interface IReportRepository: IRepository<Report>
{
    Task<Report?> GetByIDAsync(Guid id,bool withsubdata = true);
    Task<IEnumerable<Report>> ActiveAsync(CancellationToken cancellationToken);
    Task<bool> FindAsync(Report report);
}
