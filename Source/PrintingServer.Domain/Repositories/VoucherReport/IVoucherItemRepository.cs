using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;

public interface IVoucherItemRepository: IRepository<VoucherItem>
{
    Task<VoucherItem?> GetByIDAsync(Guid id);
}
