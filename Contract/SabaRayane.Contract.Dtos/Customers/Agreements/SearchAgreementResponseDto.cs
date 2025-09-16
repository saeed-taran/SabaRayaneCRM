namespace SabaRayane.Contract.Dtos.Customers.Agreements;

public record SearchAgreementResponseDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public long Price { get; set; }
    public string AgreementDate { get; set; }
    public int DurationInMonths { get; set; }
    public int ExtraUsersCount { get; set; }

    public SearchAgreementResponseDto(int id, int customerId, string customerName, int productId, string productName, long price, string agreementDate, int durationInMonths, int extraUsersCount)
    {
        Id = id;
        CustomerId = customerId;
        CustomerName = customerName;
        ProductId = productId;
        ProductName = productName;
        Price = price;
        AgreementDate = agreementDate;
        DurationInMonths = durationInMonths;
        ExtraUsersCount = extraUsersCount;
    }
}
