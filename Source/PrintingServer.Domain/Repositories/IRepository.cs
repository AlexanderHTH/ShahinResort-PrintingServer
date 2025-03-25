using PrintingServer.Domain.Common;

namespace PrintingServer.Domain.Repositories;

public interface IRepository<T> where T : class 
{
    Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken);
    Task<T> Create(T entity, CancellationToken cancellationToken);
    Task Create(IEnumerable<T> entities, CancellationToken cancellationToken);
    Task Delete(T entity, CancellationToken cancellationToken);
    Task Delete(IEnumerable<T> entities, CancellationToken cancellationToken);
    Task Update(T entity, CancellationToken cancellationToken);
    Task<bool> IsEmpty();
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<IPagedResult<T>> WhereAsync(ISearchDTO<T> searchDTO, CancellationToken cancellationToken);
}
