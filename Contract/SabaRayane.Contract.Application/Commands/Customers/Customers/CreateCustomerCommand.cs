using SabaRayane.Contract.Dtos.s.Customers;
using Taran.Shared.Application.Commands;

namespace SabaRayane.Contract.Application.Commands.s.Customers;

public record CreateCustomerCommand : CreateCustomerRequestDto, ICommandWithUser<bool>
{
}
