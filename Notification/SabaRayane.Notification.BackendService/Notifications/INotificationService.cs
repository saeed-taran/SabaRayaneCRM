using SabaRayane.Notification.Dtos.Progress;

namespace SabaRayane.Notification.BackendService.Notifications;

public interface INotificationService
{
    Task SendTaskStatus(int userId, PushTaskStatusDto pushTaskStatusDto);
}
