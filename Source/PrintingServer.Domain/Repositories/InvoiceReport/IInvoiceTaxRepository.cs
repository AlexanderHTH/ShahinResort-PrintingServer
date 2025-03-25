using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IInvoiceTaxRepository: IRepository<InvoiceTax>
{
    Task<InvoiceTax?> GetByIDAsync(Guid id);
}
