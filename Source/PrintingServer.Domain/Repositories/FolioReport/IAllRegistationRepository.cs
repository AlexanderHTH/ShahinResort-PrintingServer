using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IAllRegistationRepository : IRepository<AllRegistration>
{
    Task<AllRegistration?> GetByIDAsync(Guid id);
}
