namespace PrintingServer.Infrastructure.Seeders;

public interface IPrintServerSeeder
{
    Task SeedAsync(CancellationToken cancellationToken);
}
