using Taran.Shared.Dtos.Languages;
using System.ComponentModel.DataAnnotations;
using Taran.Shared.Application.Commands;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.Commands.Products.Products;

public record ImportProductCommand : RequestWithUserDtoBase, ICommandWithUser<bool>
{
    [Required(ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public string Name { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public long Price { get; set; }

    public string? Description { get; set; }
}