using FluentValidation;
using PrintingServer.API.Extentions;
using PrintingServer.API.MiddleWares;
using PrintingServer.Infrastructure.Seeders;
using Serilog;

namespace PrintingServer.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AppPresentation();
            // Add services to the container.
            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IPrintServerSeeder>();
            var cts = new CancellationTokenSource();
            await seeder.SeedAsync(cts.Token);
            // Configure the HTTP request pipeline.
            app.UseMiddleware<ErrorHandlingMiddleWare>();
            //     app.UseMiddleware<GetIPRemoteAddressMiddleWare>();
            //if (app.Environment.IsDevelopment())
            //{
                app.MapOpenApi();
            //}
            app.UseSerilogRequestLogging();
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}
            //app.MapGroup("api/UserManagment").WithTags("Identity").MapIdentityApi<AppUser>();
            app.MapControllers();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            var services = builder.Services.AsQueryable().OrderBy(s => s.ServiceType.ToString()).ToList();
            Console.WriteLine($"Registered Services: ({services.Count()})");
            Console.WriteLine($"Registered Services: MicroSoft ({services.Where(s=>s.ServiceType.ToString().Contains("Microsoft")).Count()})");
            Console.WriteLine($"Registered Services: Fluent ({services.Where(s=>s.ServiceType.ToString().Contains("Fluent")).Count()})");
            Console.WriteLine($"Registered Services: Serilog ({services.Where(s => s.ServiceType.ToString().Contains("Serilog")).Count()})");

            Console.WriteLine("-------------------------------------------------------------------------------------------");
            app.Run();
        }
    }
}
