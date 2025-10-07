using Taran.Shared.Application.Commands;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.Commands.Customers.CustomerAggregate;

public record DeleteNotificationCommand : CommonDeleteRequestDto, ICommandWithUser<bool>
{
}
