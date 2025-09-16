using SabaRayane.Contract.Dtos.Customers.Agreements;
using Taran.Shared.Application.Commands;

namespace SabaRayane.Contract.Application.Commands.Customers.Agreements;

public record UpdateAgreementCommand : UpdateAgreementRequestDto, ICommandWithUser<bool>
{
}
