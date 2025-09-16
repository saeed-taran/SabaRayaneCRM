using SabaRayane.Contract.Dtos.Customers.Agreements;
using Taran.Shared.Application.Commands;

namespace SabaRayane.Contract.Application.Commands.Customers.Agreements;

public record CreateAgreementCommand : CreateAgreementRequestDto, ICommandWithUser<bool>
{
}
