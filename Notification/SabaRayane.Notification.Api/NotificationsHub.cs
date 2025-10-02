using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SabaRayane.Notification.Dtos.NotificationClient;
using Taran.Shared.Core.User;

namespace SabaRayane.Notification.Api;

[Authorize]
public class NotificationsHub : Hub<INotificationClient>
{
    private readonly IAppUser appUser;

    public NotificationsHub(IAppUser appUser)
    {
        this.appUser = appUser;
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
}