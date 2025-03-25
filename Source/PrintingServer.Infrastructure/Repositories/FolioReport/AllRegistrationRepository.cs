using Microsoft.EntityFrameworkCore;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories;

internal class AllRegistrationRepository(Print_DBContext dbContext) : RepositoryBase<AllRegistration>(dbContext), IAllRegistationRepository
{
    public async Task<AllRegistration?> GetByIDAsync(Guid id)
    {
        var toreturn = await _dbContext.AllRegistrations.FirstOrDefaultAsync(p => p.Id == id);
        return toreturn;
    }
}
