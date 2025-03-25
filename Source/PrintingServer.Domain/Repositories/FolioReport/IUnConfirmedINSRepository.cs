using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;
public interface IUnConfirmedINSRepository: IRepository<UnConfirmedINS>
{
    Task<UnConfirmedINS?> GetByIDAsync(Guid id);
}
