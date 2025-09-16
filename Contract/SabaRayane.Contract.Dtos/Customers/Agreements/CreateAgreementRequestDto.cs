using System.ComponentModel.DataAnnotations;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.Attributes;
using Taran.Shared.Dtos.Languages;

namespace SabaRayane.Contract.Dtos.Customers.Agreements;

public record CreateAgreementRequestDto : RequestWithUserDtoBase
{
    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public int CustomerId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public long Price { get; set; }

    [Date]
    public string AgreementDate { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public int DurationInMonths { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public int ExtraUsersCount { get; set; }
}
