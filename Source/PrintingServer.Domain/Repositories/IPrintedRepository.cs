using PrintingServer.Domain.Entities;

namespace PrintingServer.Domain.Repositories;

public interface IPrintedRepository : IRepository<Printed>
{
    Task<Printed?> GetByIDAsync(Guid id);
}
