using Microsoft.Extensions.Options;
using SabaRayane.Notification.Dtos.Progress;
using System.Net.Http.Json;

namespace SabaRayane.Notification.BackendService.Notifications;

public class NotificationService : INotificationService
{
    private readonly NotificationConfiguration notificationConfiguration;
    private readonly HttpClient httpClient;

    public NotificationService(IOptions<NotificationConfiguration> notificationConfiguration, IHttpClientFactory httpClientFactory)
    {
        this.notificationConfiguration = notificationConfiguration.Value;
        httpClient = httpClientFactory.CreateClient(this.notificationConfiguration.ApiAddress);
    }

    public async Task SendTaskStatus(int userId, PushTaskStatusDto pushTaskStatusDto)
    {
        await httpClient.PostAsJsonAsync($"{notificationConfiguration.ApiAddress}/Notification/TaskStatus/{userId}", pushTaskStatusDto);
    }
}
