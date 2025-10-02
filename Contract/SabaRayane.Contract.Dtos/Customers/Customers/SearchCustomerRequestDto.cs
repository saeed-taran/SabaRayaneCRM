using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Dtos.s.Customers;

public record SearchCustomerRequestDto : FilterRequestWithUserDtoBase
{
    public int? CustomerId { get; set; }
}
