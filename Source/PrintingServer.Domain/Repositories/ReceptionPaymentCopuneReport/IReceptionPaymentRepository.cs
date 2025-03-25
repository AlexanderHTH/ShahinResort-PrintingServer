using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IReceptionPaymentRepository: IRepository<ReceptionPayment>
{
    Task<ReceptionPayment?> GetByIDAsync(Guid id);
}
