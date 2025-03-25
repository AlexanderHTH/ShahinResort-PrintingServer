using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;

public interface ICountryRepository : IRepository<Country>
{
    Task<Country?> GetByIDAsync(Guid id);
    Task<bool> FindAsync(Country country);
    Task<IEnumerable<Country>> ActiveAsync(CancellationToken cancellationToken);
}
