using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;

public interface IPrinterErrorLogRepository : IRepository<PrinterErrorLog>
{
    Task<PrinterErrorLog?> GetByIDAsync(Guid id);
}
