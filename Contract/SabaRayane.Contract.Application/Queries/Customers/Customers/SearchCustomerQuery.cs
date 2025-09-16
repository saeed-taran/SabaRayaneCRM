using SabaRayane.Contract.Dtos.s.Customers;
using Taran.Shared.Application.Queries;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.Queries.s.Customers;

public record SearchCustomerQuery : SearchCustomerRequestDto, IQueryWithUser<PaginatedResponseDto<SearchCustomerResponseDto>>
{
}
