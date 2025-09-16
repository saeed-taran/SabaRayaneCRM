using SabaRayane.Contract.Core.Aggregates.ProductAggregate;
using Taran.Shared.Core.Entity;
using Taran.Shared.Core.Exceptions;

namespace SabaRayane.Contract.Core.Aggregates.CustomerAggregate;

public class Agreement : BaseEntity<int>
{
    public int CustomerId { get; private set; }
    public Customer Customer { get; private set; }

    public int ProductId { get; private set; }
    public Product Product { get; private set; }

    public long Price { get; private set; }

    public DateOnly AgreementDate { get; private set; }

    public int DurationInMonths { get; private set; }

    public int ExtraUsersCount { get; private set; }

    internal Agreement(int creatorUserId, int productId, long price, DateOnly agreementDate, int durationInMonths, int extraUsersCount)
        : base(creatorUserId)
    {
        ProductId = productId;
        Price = price > 0 ? price : throw new DomainInvalidArgumentException(nameof(Price));
        AgreementDate = agreementDate;
        DurationInMonths = durationInMonths > 0 ? durationInMonths : throw new DomainInvalidArgumentException(nameof(DurationInMonths));
        ExtraUsersCount = extraUsersCount >= 0 ? extraUsersCount : throw new DomainInvalidArgumentException(nameof(ExtraUsersCount));
    }

    public void Update(int userId, int productId, DateOnly agreementDate, int durationInMonths, int extraUsersCount) 
    {
        ProductId = productId;
        AgreementDate = agreementDate;
        DurationInMonths = durationInMonths > 0 ? durationInMonths : throw new DomainInvalidArgumentException(nameof(DurationInMonths));
        ExtraUsersCount = extraUsersCount >= 0 ? extraUsersCount : throw new DomainInvalidArgumentException(nameof(ExtraUsersCount));

        Update(userId);
    }
}
