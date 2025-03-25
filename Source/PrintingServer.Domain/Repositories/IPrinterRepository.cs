using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;

public interface IPrinterRepository : IRepository<Printer>
{
    Task<Printer?> GetByIDAsync(Guid id, bool withsubdata = true);
    Task<Printer?> GetByPrinterNameAsync(string printername, bool withsubdata = true);
    Task<Printer?> GetByPrinterIPAsync(string printerip, bool withsubdata = true);
    Task<bool> FindAsync(Printer printer);
    Task<IEnumerable<Printer>> ActiveAsync(CancellationToken cancellationToken);
}
