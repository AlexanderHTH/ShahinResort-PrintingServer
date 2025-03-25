using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IUnConfirmedPaymentRepository : IRepository<UnConfirmedPayment>
{
    Task<UnConfirmedPayment?> GetByIDAsync(Guid id);
}
