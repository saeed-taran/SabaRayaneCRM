using Taran.Shared.Application.Commands;

namespace SabaRayane.Contract.Application.Commands.Customers.Notifications;

public record CreatePossibleNotificationsCommand : ICommandWithoutUser<bool>
{
}