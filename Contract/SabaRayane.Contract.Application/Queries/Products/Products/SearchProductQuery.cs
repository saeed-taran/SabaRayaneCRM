using SabaRayane.Contract.Dtos.s.Products;
using Taran.Shared.Application.Queries;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.Queries.s.Products;

public record SearchProductQuery : SearchProductRequestDto, IQueryWithUser<PaginatedResponseDto<SearchProductResponseDto>>
{
}
