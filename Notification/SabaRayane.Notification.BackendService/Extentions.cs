using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SabaRayane.Notification.BackendService.Notifications;

namespace SabaRayane.Notification.BackendService;

public static class NotificationExtentions
{
    public static IServiceCollection AddNotificationService(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var notificationConfigurationSection = configuration.GetSection(nameof(NotificationConfiguration));
        NotificationConfiguration notificationConfiguration = new();
        notificationConfigurationSection.Bind(notificationConfiguration);
        services.Configure<NotificationConfiguration>(notificationConfigurationSection);

        services.AddHttpClient();

        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }
}
