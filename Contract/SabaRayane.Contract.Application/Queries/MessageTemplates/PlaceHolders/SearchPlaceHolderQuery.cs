using SabaRayane.Contract.Dtos.MessageTemplates.PlaceHolders;
using Taran.Shared.Application.Queries;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.Queries.MessageTemplates.PlaceHolders;

public record SearchPlaceHolderQuery : FilterRequestWithUserDtoBase, IQueryWithUser<PaginatedResponseDto<SearchPlaceHolderResponseDto>>
{
}
