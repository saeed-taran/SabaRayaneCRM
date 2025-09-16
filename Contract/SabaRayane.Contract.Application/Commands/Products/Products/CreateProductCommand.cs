using SabaRayane.Contract.Dtos.s.Products;
using Taran.Shared.Application.Commands;

namespace SabaRayane.Contract.Application.Commands.s.Products;

public record CreateProductCommand : CreateProductRequestDto, ICommandWithUser<bool>
{
}
