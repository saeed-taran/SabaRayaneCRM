using SabaRayane.Contract.Dtos.s.MessageTemplates;
using Taran.Shared.Application.Queries;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.Queries.s.MessageTemplates;

public record SearchMessageTemplateQuery : SearchMessageTemplateRequestDto, IQueryWithUser<PaginatedResponseDto<SearchMessageTemplateResponseDto>>
{
}
