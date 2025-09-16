using SabaRayane.Contract.Dtos.Customers.Agreements;
using Taran.Shared.Application.Queries;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.Queries.Customers.Agreements;

public record SearchAgreementQuery : SearchAgreementRequestDto, IQueryWithUser<PaginatedResponseDto<SearchAgreementResponseDto>>
{
}
