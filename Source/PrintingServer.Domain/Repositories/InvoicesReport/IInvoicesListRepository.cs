using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IInvoicesListRepository: IRepository<InvoicesList>
{
    Task<InvoicesList?> GetByIDAsync(Guid id);
}
