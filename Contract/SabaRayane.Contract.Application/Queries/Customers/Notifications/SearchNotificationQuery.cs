using SabaRayane.Contract.Dtos.Customers.CustomerAggregate;
using Taran.Shared.Application.Queries;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.Queries.Customers.CustomerAggregate;

public record SearchNotificationQuery : SearchNotificationRequestDto, IQueryWithUser<PaginatedResponseDto<SearchNotificationResponseDto>>
{
}
