using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IInvoiceItemRepository : IRepository<InvoiceItem>
{
    Task<InvoiceItem?> GetByIDAsync(Guid id);
}
