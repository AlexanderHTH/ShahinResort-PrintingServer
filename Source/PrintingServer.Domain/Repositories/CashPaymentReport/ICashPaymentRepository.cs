using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface ICashPaymentRepository : IRepository<CashPayment>
{
    Task<CashPayment?> GetByIDAsync(Guid id);
}
