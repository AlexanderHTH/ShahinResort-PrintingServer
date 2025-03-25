using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IInvoiceRepository: IRepository<Invoice>
{
    Task<Invoice?> GetByIDAsync(Guid id);
}
