using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IConfirmedINSRepository : IRepository<ConfirmedINS>
{
    Task<ConfirmedINS?> GetByIDAsync(Guid id);
}
