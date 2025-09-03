using Taran.Shared.Dtos.Languages;
using System.ComponentModel.DataAnnotations;

namespace Taran.Shared.Dtos;

public record CommonDeleteRequestDto : RequestWithUserDtoBase
{
    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.IdIsMissing))]
    public int Id { get; set; }
}
