using System.ComponentModel.DataAnnotations;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.Attributes;
using Taran.Shared.Dtos.Languages;

namespace SabaRayane.Contract.Dtos.s.Customers;

public record CreateCustomerRequestDto : RequestWithUserDtoBase
{
    [Required(ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public string CustomerId { get; set; }

    [Required(ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public string FirstName { get; set; }

    [Required(ErrorMessage = nameof(KeyWords.FieldIsRequired))]
    public string LastName { get; set; }

    public string? StoreName { get; set; }

    [Mobile]
    public string? MobileNumber { get; set; }
    public string? Description { get; set; }
}
