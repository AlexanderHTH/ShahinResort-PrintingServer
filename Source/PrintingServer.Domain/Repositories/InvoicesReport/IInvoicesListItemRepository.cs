using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IInvoicesListItemRepository: IRepository<InvoicesListItem>
{
    Task<InvoicesListItem?> GetByIDAsync(Guid id);
}
