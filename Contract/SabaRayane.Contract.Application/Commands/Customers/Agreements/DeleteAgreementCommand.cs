using Taran.Shared.Application.Commands;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.Commands.Customers.Agreements;

public record DeleteAgreementCommand : CommonDeleteRequestDto, ICommandWithUser<bool>
{
}
