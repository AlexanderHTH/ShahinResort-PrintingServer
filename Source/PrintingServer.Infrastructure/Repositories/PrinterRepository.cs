using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class PrinterRepository(Print_DBContext dbContext) : RepositoryBase<Printer>(dbContext), IPrinterRepository
{
    public async Task<IEnumerable<Printer>> ActiveAsync(CancellationToken cancellationToken)
    {
        var toreturn = await _dbContext.Printers.Where(printer => printer.IsActive).ToListAsync(cancellationToken);
        return toreturn;
    }

    public async Task<bool> FindAsync(Printer printer)
    {
        var toreturn = await _dbContext.Printers.FirstOrDefaultAsync(p=> p.PrinterName == printer.PrinterName || p.PrinterIp== printer.PrinterIp);
        return toreturn != null;
    }

    public async Task<Printer?> GetByIDAsync(Guid id,bool withsubdata=true)
    {
        var toreturn = await _dbContext.Printers.Include(p => p.PrinterErrorLogs)
                                               .Include(p => p.ClientReports)
                                               .FirstOrDefaultAsync(o => o.Id == id);
        if (toreturn != null && !withsubdata)
        {
            toreturn.ClientReports = null;
            toreturn.PrinterErrorLogs = null;
        }

        return toreturn;
    }

    public async Task<Printer?> GetByPrinterIPAsync(string printerip, bool withsubdata=true)
    {
        var toreturn = await _dbContext.Printers.Include(p => p.PrinterErrorLogs)
                                               .Include(p => p.ClientReports)
                                               .FirstOrDefaultAsync(o => o.PrinterIp == printerip);
        if (toreturn != null && !withsubdata)
        {
            toreturn.ClientReports = null;
            toreturn.PrinterErrorLogs = null;
        }

        return toreturn;
    }

    public async Task<Printer?> GetByPrinterNameAsync(string printername, bool withsubdata = true)
    {
        var toreturn = await _dbContext.Printers.Include(p => p.PrinterErrorLogs)
                                       .Include(p => p.ClientReports)
                                       .FirstOrDefaultAsync(o => o.PrinterName == printername);
        if (toreturn != null && !withsubdata)
        {
            toreturn.ClientReports = null;
        }
        return toreturn;
    }
}
