using Microsoft.AspNetCore.SignalR;
using Taran.Shared.Core.User;

namespace SabaRayane.Notification.Api;

public class SignalRUserIdProvider : IUserIdProvider
{
    private readonly IServiceProvider _serviceProvider;

    public SignalRUserIdProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public string? GetUserId(HubConnectionContext connection)
    {
        using var scope = _serviceProvider.CreateScope();
        var appUser = scope.ServiceProvider.GetRequiredService<IAppUser>();

        return appUser?.UserID.ToString();
    }
}
