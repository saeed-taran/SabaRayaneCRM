using Taran.Shared.Application.Commands;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.Commands.s.Customers;

public record DeleteCustomerCommand : CommonDeleteRequestDto, ICommandWithUser<bool>
{
}
