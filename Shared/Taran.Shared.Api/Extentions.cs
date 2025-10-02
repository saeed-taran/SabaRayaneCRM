using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Net;
using Taran.Shared.Api.Configurations;
using Taran.Shared.Api.JWT;
using Taran.Shared.Api.Middlewares;
using Taran.Shared.Api.User;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.User;
using Taran.Shared.Dtos.Attributes;
using Taran.Shared.Dtos.Services.Calendar;
using Taran.Shared.Dtos.WrappedResponse;
using Taran.Shared.Infrastructure.Configurations;
using Taran.Shared.Language;
using Taran.Shared.Languages;

namespace Taran.Shared.Api;

public static class Extentions
{
    public static IServiceCollection AddCustomeAuthentication(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var identityConfigurationSection = configuration.GetSection(nameof(IdentityConfiguration));
        IdentityConfiguration identityConfiguration = new();
        identityConfigurationSection.Bind(identityConfiguration);

        services.Configure<IdentityConfiguration>(identityConfigurationSection);

        services.AddScoped<IAppUser, AppUser>();
        services.AddTransient<AuthorizeMiddleware>();
        services.AddScoped<IJWTManager, JWTManager>();

        JwtBearerDefaultOptions jwtBearerDefaultOptions = new JwtBearerDefaultOptions(identityConfiguration.JWTKey);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtBearerDefaultOptions.SetOptions);

        return services;
    }

    public static IServiceCollection InitCalendar(this IServiceCollection services, IConfigurationRoot configuration) 
    {
        var identityConfigurationSection = configuration.GetSection(nameof(CultureConfiguration));
        if (identityConfigurationSection is null)
            throw new ArgumentNullException(nameof(CultureConfiguration));

        CultureConfiguration cultureConfiguration = new();
        identityConfigurationSection.Bind(cultureConfiguration);

        if (cultureConfiguration.DateTime == "Shamsi")
        {
            services.AddScoped<ICalendar, PersianCalendar>();
            DateAttribute.Calendar = new PersianCalendar();
        }
        else
        {
            services.AddScoped<ICalendar, GeorgianCalendar>();
            DateAttribute.Calendar = new GeorgianCalendar();
        }

        return services;
    }

    public static IServiceCollection InitTranslator(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var cultureConfigurationSection = configuration.GetSection(nameof(CultureConfiguration));
        if (cultureConfigurationSection is null)
            throw new ArgumentNullException(nameof(CultureConfiguration));

        CultureConfiguration cultureConfiguration = new();
        cultureConfigurationSection.Bind(cultureConfiguration);

        var languagePacksPath = Path.Combine(AppContext.BaseDirectory, "Resources", "Languages");
        if (Directory.GetFiles(languagePacksPath, "*.yml").Count() == 0)
            throw new FileNotFoundException(languagePacksPath);

        services.AddSingleton<ITranslator>(
            new TranslatorForBackend(cultureConfiguration, languagePacksPath)
        );

        return services;
    }

    public static IServiceCollection AddConnectionStringConfiguration(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var connectionStringConfiguration = configuration.GetSection("ConnectionStrings");
        ConnectionStringsConfiguration connectionStringConfig = new();
        connectionStringConfiguration.Bind(connectionStringConfig);

        services.Configure<ConnectionStringsConfiguration>(connectionStringConfiguration);

        return services;
    }

    public static IApplicationBuilder UseCustomeAuthorizeMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<AuthorizeMiddleware>();
    }

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services) 
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });

        return services;
    }

    public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
    {
        return app.UseCors("CorsPolicy");
    }

    public static IServiceCollection AddSwaggerGenWithAuthorize(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
        });

        return services;
    }

    public static IConfigurationRoot LoadConfigurations(WebApplicationBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", false)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false)
            .AddJsonFile($"appsettings.shared.json", false)
            .AddJsonFile($"appsettings.shared.{builder.Environment.EnvironmentName}.json", false)
            .Build();

        return configuration;
    }

    public static void AddQueries(this IServiceCollection services, Type typeToGetAssembly)
    {
        foreach (var type in typeToGetAssembly.Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Queries")))
        {
            var i = type.GetInterfaces();
            var _interface = i.First(v => v.Name == "I" + type.Name);
            services.AddTransient(_interface, type);
        }
    }

    public static void AddExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(c => c.Run(async context =>
        {
            Exception? exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

            if (exception is BaseDomainException)
            {
                var ex = (BaseDomainException)exception;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(new ErrorResponse(ex.Message));
            }
            else if(exception is not null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new ErrorResponse(exception.Message));
            }
        }));
    }

    private const string NullIpAddress = "::1";
    public static bool IsLocal(this HttpRequest req)
    {
        var connection = req.HttpContext.Connection;
        if (connection.RemoteIpAddress.IsSet())
        {
            return connection.LocalIpAddress.IsSet()
                ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress)
                : IPAddress.IsLoopback(connection.RemoteIpAddress);
        }

        return true;
    }
    private static bool IsSet(this IPAddress address)
    {
        return address != null && address.ToString() != NullIpAddress;
    }
}