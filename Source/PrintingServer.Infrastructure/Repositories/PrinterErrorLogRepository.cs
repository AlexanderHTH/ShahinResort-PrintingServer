using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class PrinterErrorLogRepository(Print_DBContext dbContext) : RepositoryBase<PrinterErrorLog>(dbContext), IPrinterErrorLogRepository
{
    public async Task<PrinterErrorLog?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.PrinterErrorLogs.FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}
