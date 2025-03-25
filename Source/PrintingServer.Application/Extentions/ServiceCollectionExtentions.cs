using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PrintingServer.Application.UsersManagment;
using Serilog;

namespace PrintingServer.Application.Extentions;
public static class ServiceCollectionExtentions
{
    private static void RegisterMapper(this IServiceCollection services, Assembly appAssembly)
    {
        Console.WriteLine("Setting Mapper ..... ");
        Console.WriteLine("-------------------------------------------------------------------------------------------");
        services.AddAutoMapper(cfg =>
                {
                    cfg.AllowNullCollections = true;
                    cfg.AddGlobalIgnore("Item");
                },
                appAssembly);
    }
    private static void RegisterValidation(this IServiceCollection services, Assembly appAssembly)
    {
        Console.WriteLine("Setting Fluent Validation ..... ");
        Console.WriteLine("-------------------------------------------------------------------------------------------");
        services.AddValidatorsFromAssembly(appAssembly).AddFluentValidationAutoValidation();
        //services.AddValidatorsFromAssemblyContaining(typeof(SearchDTOValidator<,>));

        //// Get all concrete types that inherit from BaseEntity
        //var allTypes = AppDomain.CurrentDomain.GetAssemblies()
        //                                     .SelectMany(a => a.GetTypes())
        //                                     .Where(t => t.IsSubclassOf(typeof(BaseEntity)) && !t.IsAbstract); // Exclude abstract classes

        //// Register SearchDTOValidator for each concrete type that inherits from BaseEntity
        //foreach (var type in allTypes)
        //{
        //    // Create the specific SearchDTO<T> type
        //    var searchDTOType = typeof(SearchDTO<>).MakeGenericType(type);

        //    // Create the specific SearchDTOValidator<T> type
        //    var validatorType = typeof(SearchDTOValidator<>).MakeGenericType(type);

        //    // Register the SearchDTOValidator for the specific type
        //    services.AddScoped(typeof(IValidator<>).MakeGenericType(searchDTOType), validatorType);
        //}

        //// Register ValidationBehavior globally for MediatR
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
    private static void ConfigurateSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
        builder.Services.AddSingleton(typeof(ITQLogger<>), typeof(TQLogger<>));
    }
    public static void AddApplication(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var applicationAssembly = typeof(ServiceCollectionExtentions).Assembly;
        Console.WriteLine("Setting MediatR ..... ");
        Console.WriteLine("-------------------------------------------------------------------------------------------");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));
        services.RegisterMapper(applicationAssembly);
        services.RegisterValidation(applicationAssembly);
        builder.ConfigurateSerilog();
        services.AddScoped<IUserContext, UserContext>();
        services.AddHttpContextAccessor();
    }
}
