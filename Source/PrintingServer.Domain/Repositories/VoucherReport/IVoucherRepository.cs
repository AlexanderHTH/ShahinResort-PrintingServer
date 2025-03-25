using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;

public interface IVoucherRepository: IRepository<Voucher>
{
    Task<Voucher?> GetByIDAsync(Guid id);
}