using Taran.Shared.Application.Commands;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.Commands.s.Products;

public record DeleteProductCommand : CommonDeleteRequestDto, ICommandWithUser<bool>
{
}
