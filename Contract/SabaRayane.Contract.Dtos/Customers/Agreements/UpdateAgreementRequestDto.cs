using System.ComponentModel.DataAnnotations;
using Taran.Shared.Dtos.Languages;

namespace SabaRayane.Contract.Dtos.Customers.Agreements;

public record UpdateAgreementRequestDto : CreateAgreementRequestDto
{
    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public int Id { get; set; }
}
