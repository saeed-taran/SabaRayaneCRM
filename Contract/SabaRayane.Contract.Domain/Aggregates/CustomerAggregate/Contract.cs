using SabaRayane.Contract.Domain.Aggregates.ProductAggregate;
using Taran.Shared.Core.Entity;
using Taran.Shared.Core.Exceptions;

namespace SabaRayane.Contract.Domain.Aggregates.CustomerAggregate;

public class Contract : BaseEntity<int>
{
    public int CustomerId { get; private set; }
    public Customer Customer { get; private set; }

    public int ProductId { get; private set; }
    public Product Product { get; private set; }

    public long Price { get; private set; }

    public DateOnly ContractDate { get; private set; }

    public int DurationInMonths { get; private set; }

    public int ExtraUsersCount { get; private set; }

    internal Contract(int creatorUserId, int productId, long price, DateOnly contractDate, int durationInMonths, int extraUsersCount)
        : base(creatorUserId)
    {
        ProductId = productId;
        Price = price > 0 ? price : throw new DomainInvalidArgumentException(nameof(Price));
        ContractDate = contractDate;
        DurationInMonths = durationInMonths > 0 ? durationInMonths : throw new DomainInvalidArgumentException(nameof(DurationInMonths));
        ExtraUsersCount = extraUsersCount >= 0 ? extraUsersCount : throw new DomainInvalidArgumentException(nameof(ExtraUsersCount));
    }
}
