using SabaRayane.Notification.Dtos.Progress;

namespace SabaRayane.Notification.Dtos.NotificationClient;

// this interface is only for enforce strong typing for client-side, no need for implementation
public interface INotificationClient
{
    Task PushTaskStatus(PushTaskStatusDto pushTaskStatusDto);
}
