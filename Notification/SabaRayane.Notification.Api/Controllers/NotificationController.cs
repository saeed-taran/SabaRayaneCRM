using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SabaRayane.Notification.Dtos.NotificationClient;
using SabaRayane.Notification.Dtos.Progress;
using Taran.Shared.Api.Controllers;

namespace SabaRayane.Notification.Api.Controllers
{
    public class NotificationController : UnAuthorizedControllerBase
    {
        private readonly IHubContext<NotificationsHub, INotificationClient> hubContext;

        public NotificationController(IHubContext<NotificationsHub, INotificationClient> hubContext) : base(null)
        {
            this.hubContext = hubContext;
        }

        [HttpPost("TaskStatus/{userId}")]
        //TODO [OnlyLocal]
        public async Task<ActionResult> TaskStatus([FromRoute] int userId, [FromBody] PushTaskStatusDto pushTaskStatusDto)
        {
            await hubContext.Clients.User(userId.ToString()).PushTaskStatus(pushTaskStatusDto);
            return Ok();
        }
    }
}