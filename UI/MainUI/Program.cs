using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AKSoftware.Localization.MultiLanguages;
using System.Reflection;
using MudBlazor.Services;
using Taran.Shared.UI.Languages;
using Taran.UI.Main;
using Taran.Shared.UI.Services;
using Taran.Shared.UI.Services.HttpService;
using Taran.Shared.Dtos.Services.Calendar;
using Taran.Shared.UI.Extensions;
using Taran.Shared.UI.Auth;
using Taran.Shared.UI.ConfigurationModels;
using Taran.Shared.Dtos.ConfigurationModels;
using Taran.UI.Main.Services.IdentityServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var envName = builder.HostEnvironment.IsDevelopment() ? "Development" : "Production";

var configuration = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", false)
    .AddJsonFile($"appsettings.{envName}.json", false)
    .Build();

builder.Services.AddScoped(sp => configuration.GetSection(nameof(NavigationConfiguration)).Get<NavigationConfiguration>()!);
builder.Services.AddScoped(sp => configuration.GetSection(nameof(CultureConfiguration)).Get<CultureConfiguration>()!);

builder.Services.AddMudServices();

builder.Services.AddLanguageContainer(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ITranslator, Translator>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<HttpClient>();

builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<ICookieStorageService, CookieStorageService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<ICalendar, PersianCalendar>();
builder.Services.AddScoped<IToastService, ToastService>();

builder.Services.AddServices(typeof(RoleService));

// Auth
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<AuthStateProvider>();

await builder.Build().RunAsync();
