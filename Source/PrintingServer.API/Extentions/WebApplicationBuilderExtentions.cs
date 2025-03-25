using System.Reflection;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrintingServer.API.MiddleWares;
using PrintingServer.Application.Extentions;
using PrintingServer.Domain.Entities.UserEntities;
using PrintingServer.Infrastructure.Authorization;
using PrintingServer.Infrastructure.Extentions;

namespace PrintingServer.API.Extentions;
public static class WebApplicationBuilderExtentions
{
    private static void SwaggerGenerator(this IServiceCollection services)
    {
        Console.WriteLine("Setting Swagger ..... ");
        Console.WriteLine("-------------------------------------------------------------------------------------------");
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Suiter Print Server API",
                Description = "An ASP.NET Core Web API for managing printing reports for Shahin Resort Suiter.",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Hossam T. Hanna",
                    Url = new Uri("https://example.com/contact"),
                    Email = "eng.hossamhanna@gmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "TQSystems License",
                    Url = new Uri("https://example.com/license")
                }
            });
            // Enable XML Comments
            var basePath = AppContext.BaseDirectory;
            var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
            if (File.Exists(apiXmlPath))
            {
                options.IncludeXmlComments(apiXmlPath);
            }
            else
            {
                Console.WriteLine($"Warning: API XML file not found at {apiXmlPath}");
            }
            var appXmlFile = "PrintingServer.Application.xml"; // Name of the Application project XML
            var appXmlPath = Path.Combine(basePath, appXmlFile);
            if (File.Exists(appXmlPath))
            {
                options.IncludeXmlComments(appXmlPath);
            }
            else
            {
                Console.WriteLine($"Warning: Application XML file not found at {appXmlPath}");
            }
            var domXmlFile = "PrintingServer.Domain.xml"; // Name of the Domain project XML
            var domXmlPath = Path.Combine(basePath, domXmlFile);
            if (File.Exists(domXmlPath))
            {
                options.IncludeXmlComments(domXmlPath);
            }
            else
            {
                Console.WriteLine($"Warning: Domain XML file not found at {domXmlPath}");
            }
            var infraXmlFile = "PrintingServer.Infrastructure.xml"; // Name of the Infrastructure project XML
            var infraXmlPath = Path.Combine(basePath, infraXmlFile);
            if (File.Exists(infraXmlPath))
            {
                options.IncludeXmlComments(infraXmlPath);
            }
            else
            {
                Console.WriteLine($"Warning: Infrastructure XML file not found at {infraXmlPath}");
            }
            // Add Security
            options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme ,
                                        Id = "bearerAuth"
                                    }
                            },
                        []
                    }
            });
        });
    }
    private static void AuthenticationGenerator(this WebApplicationBuilder builder)
    {
        Console.WriteLine("Setting Authentication ..... ");
        Console.WriteLine("-------------------------------------------------------------------------------------------");
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                     .AddJwtBearer(options =>
                     {
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuerSigningKey = true,
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                             ValidateIssuer = true,
                             ValidateAudience = true,
                             ValidIssuer = builder.Configuration["Jwt:Issuer"],
                             ValidAudience = builder.Configuration["Jwt:Audience"],
                             ClockSkew = TimeSpan.Zero
                         };
                         options.Events = new JwtBearerEvents
                         {
                             OnTokenValidated = async context =>
                             {
                                 var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<AppUser>>();
                                 var userId = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                                 var tokenVersion = context.Principal?.FindFirst(Policy.TokenVersion)?.Value;

                                 if (userId == null || tokenVersion == null)
                                 {
                                     context.Fail("Unauthorized");
                                     return;
                                 }

                                 var user = await userManager.FindByIdAsync(userId);
                                 if (user == null || user.TokenVersion.ToString() != tokenVersion)
                                 {
                                     context.Fail("Token is no longer valid.");
                                 }
                             }
                         };
                     });
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
    }
    public static void AppPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.SwaggerGenerator();
        builder.Services.AddScoped<ErrorHandlingMiddleWare>();
        builder.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration, "PrintServer_DB", "PrintServer_Users_DB");
        builder.Services.AddOpenApi();
        //builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
        
        builder.AuthenticationGenerator();
    }
}
