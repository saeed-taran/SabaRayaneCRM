using System.ComponentModel.DataAnnotations;
using Taran.Shared.Dtos.Languages;

namespace SabaRayane.Contract.Dtos.s.MessageTemplates;

public record UpdateMessageTemplateRequestDto : CreateMessageTemplateRequestDto
{
    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public int Id { get; set; }
}
