using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Dtos.Customers.Agreements;

public record SearchAgreementRequestDto : FilterRequestWithUserDtoBase
{
    public int? CustomerId { get; set; }
}
