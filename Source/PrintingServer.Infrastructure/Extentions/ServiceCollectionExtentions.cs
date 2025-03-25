using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrintingServer.Application.Common;
using PrintingServer.Domain.Authorization;
using PrintingServer.Domain.Common;
using PrintingServer.Domain.Entities;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Domain.Repositories;
using PrintingServer.Domain.Storage;
using PrintingServer.Domain.Tokens;
using PrintingServer.Infrastructure.Authorization;
using PrintingServer.Infrastructure.Authorization.Services;
using PrintingServer.Infrastructure.Presistence;
using PrintingServer.Infrastructure.Repositories;
using PrintingServer.Infrastructure.Seeders;
using PrintingServer.Infrastructure.Storage;
using PrintingServer.Infrastructure.Tokens;

namespace PrintingServer.Infrastructure.Extentions;
public static class ServiceCollectionExtentions
{
    private static void DI_Repositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
        Console.WriteLine("Setting Register Repositories ..... ");
        Console.WriteLine("-------------------------------------------------------------------------------------------");
        services.AddScoped<IAccoumodationListItemRepository, AccoumodationListItemRepository>();
        services.AddScoped<IAccoumodationListRepository, AccoumodationListRepository>();
        services.AddScoped<ICashPaymentRepository, CashPaymentRepository>();
        services.AddScoped<ICheckInListItemRepository, CheckInListItemRepository>();
        services.AddScoped<ICheckInListRepository, CheckInListRepository>();
        services.AddScoped<ICheckOutListItemRepository, CheckOutListItemRepository>();
        services.AddScoped<ICheckOutListRepository, CheckOutListRepository>();
        services.AddScoped<IDepartureListItemRepository, DepartureListItemRepository>();
        services.AddScoped<IDepartureListRepository, DepartureListRepository>();
        services.AddScoped<IAccoumodationItemRepository, AccoumodationItemRepository>();
        services.AddScoped<IAccoumodationRepository, AccoumodationRepository>();
        services.AddScoped<IExtendedArrivalListRepository, ExtendedArrivalListRepository>();
        services.AddScoped<IAllRegistationRepository, AllRegistrationRepository>();
        services.AddScoped<IConfirmedINSRepository, ConfirmedINSRepository>();
        services.AddScoped<IConfirmedPaymentRepository, ConfirmedPaymentRepository>();
        services.AddScoped<IFolioRepository, FolioRepository>();
        services.AddScoped<IRequiredItemRepository, RequiredItemRepository>();
        services.AddScoped<ITransfareRepository, TransfareRepository>();
        services.AddScoped<IUnConfirmedINSRepository, UnConfirmedINSRepository>();
        services.AddScoped<IUnConfirmedPaymentRepository, UnConfirmedPaymentRepository>();
        services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
        services.AddScoped<IInvoiceItemTaxRepository, InvoiceItemTaxRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IInvoiceTaxRepository, InvoiceTaxRepository>();
        services.AddScoped<IInvoicesListItemRepository, InvoicesListItemRepository>();
        services.AddScoped<IInvoicesListRepository, InvoicesListRepository>();
        services.AddScoped<IReceptionPaymentRepository, ReceptionPaymentRepository>();
        services.AddScoped<IReservationItemRepository, ReservationItemRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IRoomNumberRepository, RoomNumberRepository>();
        services.AddScoped<IVoucherItemRepository, VoucherItemRepository>();
        services.AddScoped<IVoucherRepository, VoucherRepository>();
        services.AddScoped<IClientReportRepository, ClientReportRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IPrintedRepository, PrintedRepository>();
        services.AddScoped<IPrinterErrorLogRepository, PrinterErrorLogRepository>();
        services.AddScoped<IPrinterRepository, PrinterRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
    }
    private static void DI_AuthorizationService(this IServiceCollection services)
    {
        Console.WriteLine("Register Authorization ..... ");
        Console.WriteLine("-------------------------------------------------------------------------------------------");
        services.AddScoped<IEntityAuthorizationService<Client>, ClientAuthorizationService>();
        services.AddScoped<IEntityAuthorizationService<ClientReport>, ClientReportAuthorizationService>();
        services.AddScoped<IEntityAuthorizationService<Country>, CountryAuthorizationService>();
        services.AddScoped<IEntityAuthorizationService<Printed>, PrintedAuthorizationService>();
        services.AddScoped<IEntityAuthorizationService<Printer>, PrinterAuthorizationService>();
        services.AddScoped<IEntityAuthorizationService<PrinterErrorLog>, PrinterErrorLogsAuthorizationService>();
        services.AddScoped<IEntityAuthorizationService<Report>, ReportAuthorizationService>();
    }
    private static void RegisterGeneralServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPagedResult<>), typeof(PagedResult<>));
        services.AddScoped(typeof(ISearchDTO<>), typeof(SearchDTO<>));
        services.AddScoped<IPrintServerSeeder, PrintServerSeeder>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
    }
    private static void ConfigurateBlob(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
        services.AddScoped<IBlobStorageService, BlobStorageService>();
    }
    private static void SettingDataBaseConnections(this IServiceCollection services, IConfiguration configuration, string mainDBName, string? usersDBName = null)
    {
        Console.WriteLine("Setting DataBase's ..... ");
        Console.WriteLine("-------------------------------------------------------------------------------------------");
        #region Set Main DB
        var connectionstring = configuration.GetConnectionString(mainDBName);
        services.AddDbContext<Print_DBContext>(options =>
            options.
                UseSqlServer(connectionstring).
                EnableSensitiveDataLogging(false));
        #endregion
        #region Set Users DB
        usersDBName ??= mainDBName;
        var users_connectionstring = configuration.GetConnectionString(usersDBName);
        services.AddDbContext<User_DBContext>(options =>
            options.
                UseSqlServer(users_connectionstring).
                EnableSensitiveDataLogging(false));
        services.AddIdentityApiEndpoints<AppUser>()
                .AddRoles<AppRole>()
                .AddClaimsPrincipalFactory<AppUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<User_DBContext>();
        // adding policies
        var builder = services.AddAuthorizationBuilder();
        foreach (string policy in Policy.PolicyList)
        {
            builder.AddPolicy(policy, builder => builder.RequireClaim(policy, "Value1", "Value2"));
        }
        // should be added in [Authorize(Roles = .... , Policy = "POLICY NAME")] in the Controller
        #endregion
    }
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string mainDBName, string? usersDBName = null)
    {
        services.SettingDataBaseConnections(configuration, mainDBName, usersDBName);
        services.DI_Repositories();
        services.DI_AuthorizationService();
        services.RegisterGeneralServices();
        services.ConfigurateBlob(configuration);
    }
}
