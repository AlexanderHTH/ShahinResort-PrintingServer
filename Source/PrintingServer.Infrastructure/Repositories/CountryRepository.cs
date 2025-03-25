using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class CountryRepository(Print_DBContext dbContext) : RepositoryBase<Country>(dbContext), ICountryRepository
{
    public async Task<IEnumerable<Country>> ActiveAsync(CancellationToken cancellationToken)
    {
        var toreturn = await _dbContext.Countries.Where(c => c.IsActive).ToListAsync(cancellationToken);
        return toreturn;
    }


    public async Task<bool> FindAsync(Country country)
    {
        var find = await _dbContext.Countries.FirstOrDefaultAsync(c => c.CountryArName == country.CountryArName || c.CountryEnName == country.CountryEnName);
        return find != null;
    }

    public async Task<Country?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.Countries.FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}