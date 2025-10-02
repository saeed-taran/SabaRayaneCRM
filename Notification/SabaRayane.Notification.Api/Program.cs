using Microsoft.AspNetCore.SignalR;
using SabaRayane.Notification.Api;
using Taran.Shared.Api;

var builder = WebApplication.CreateBuilder(args);

var configuration = Extentions.LoadConfigurations(builder);

builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

builder.Services.AddCorsPolicy();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, SignalRUserIdProvider>();

builder.Services.AddCustomeAuthentication(configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuthorize();

var app = builder.Build();

app.AddExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCorsPolicy();
app.MapHub<NotificationsHub>("notifications");

app.UseCustomeAuthorizeMiddleware();
app.MapControllers();

app.Run();