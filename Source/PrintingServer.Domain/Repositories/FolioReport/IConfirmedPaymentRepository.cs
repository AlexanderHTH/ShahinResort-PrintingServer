using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IConfirmedPaymentRepository : IRepository<ConfirmedPayment>
{
    Task<ConfirmedPayment?> GetByIDAsync(Guid id);
}
