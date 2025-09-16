namespace SabaRayane.Contract.Dtos.s.Customers;

public record SearchCustomerResponseDto
{
    public int Id { get; set; }
    public string CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? StoreName { get; set; }
    public string? MobileNumber { get; set; }
    public string? Description { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public SearchCustomerResponseDto(int id, string customerId, string firstName, string lastName, string? storeName, string? mobileNumber, string? description)
    {
        Id = id;
        CustomerId = customerId;
        FirstName = firstName;
        LastName = lastName;
        StoreName = storeName;
        MobileNumber = mobileNumber;
        Description = description;
    }
}
