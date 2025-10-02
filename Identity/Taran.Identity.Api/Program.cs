using Microsoft.EntityFrameworkCore;
using SabaRayane.Infrastructure;
using Taran.Identity.Application.Commands.Roles;
using Taran.Infrastructure;
using Taran.Shared.Api;
using Taran.Shared.Core.Repository;
using Taran.Shared.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var configuration = Extentions.LoadConfigurations(builder);

builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

builder.Services.AddCorsPolicy();
builder.Services.AddCustomeAuthentication(configuration);

builder.Services.AddDbContext<MainDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("ApplicationDb"));
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(CreateRoleCommand));
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuthorize();

/*Registering Services*/
builder.Services.AddScoped<DbContext, MainDbContext>();
builder.Services.AddScoped(typeof(IGenericReadRepository<,>), typeof(GenericReadRepository<,>));
builder.Services.AddScoped(typeof(IGenericWriteRepository<,>), typeof(GenericWriteRepository<,>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
/*************Registering Services*/

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

app.AddExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCorsPolicy();

app.UseCustomeAuthorizeMiddleware();
app.MapControllers();

app.Run();


