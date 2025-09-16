using System.ComponentModel.DataAnnotations;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.Languages;

namespace SabaRayane.Contract.Dtos.s.Products;

public record CreateProductRequestDto : RequestWithUserDtoBase
{
    [Required(ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public string Name { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public long Price { get; set; }

    public string? Description { get; set; }
}
