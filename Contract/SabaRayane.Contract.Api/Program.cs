using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Commands.s.Customers;
using SabaRayane.Notification.BackendService;
using Taran.Shared.Application.Queries.IO;
using Taran.Shared.Application.Services.IO;
using Taran.Infrastructure;
using Taran.Shared.Api;
using Taran.Shared.Core.Repository;
using Taran.Shared.Infrastructure.Repositories;
using Taran.Shared.Languages;
using SabaRayane.Contract.Infrastructure;

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
    cfg.RegisterServicesFromAssemblyContaining(typeof(CreateCustomerCommand));
    cfg.RegisterServicesFromAssemblyContaining(typeof(GetImportTemplateQuery));
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuthorize();

builder.Services.AddNotificationService(configuration);

builder.Services.InitCalendar(configuration);
builder.Services.InitTranslator(configuration);

/*Registering Services*/
builder.Services.AddScoped<DbContext, MainDbContext>();
builder.Services.AddScoped(typeof(IGenericReadRepository<,>), typeof(GenericReadRepository<,>));
builder.Services.AddScoped(typeof(IGenericWriteRepository<,>), typeof(GenericWriteRepository<,>));
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddHostedService<AgreementNotificationWorker>();
/*************Registering Services*/

var app = builder.Build();

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


