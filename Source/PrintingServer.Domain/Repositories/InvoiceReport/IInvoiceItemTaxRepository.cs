using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IInvoiceItemTaxRepository : IRepository<InvoiceItemTax>
{
    Task<InvoiceItemTax?> GetByIDAsync(Guid id);
}
