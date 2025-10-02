using System.ComponentModel.DataAnnotations;
using Taran.Shared.Dtos.Languages;

namespace Taran.Shared.Dtos;

public record CommonDeleteRequestDto : RequestWithUserDtoBase
{
    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.IdIsMissing))]
    public int Id { get; set; }
}
