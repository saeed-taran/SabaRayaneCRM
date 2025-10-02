using System.ComponentModel.DataAnnotations;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.Languages;

namespace SabaRayane.Contract.Dtos.s.MessageTemplates;

public record CreateMessageTemplateRequestDto : RequestWithUserDtoBase
{
    [Required(ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public string Name { get; set; }

    [Required(ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public string Message { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public int DaysUntilAgreementExpire { get; set; }
}
