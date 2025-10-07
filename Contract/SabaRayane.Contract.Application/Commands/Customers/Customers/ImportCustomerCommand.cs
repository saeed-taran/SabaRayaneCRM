using Taran.Shared.Application.Commands;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.Commands.Customers.Customers;

public record ImportCustomerCommand : RequestWithUserDtoBase, ICommandWithUser<bool>
{
    public string CustomerId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? StoreName { get; set; }

    public string? MobileNumber { get; set; }

    public string? Description { get; set; }
}
