using System.ComponentModel.DataAnnotations;
using Taran.Shared.Core.Entity;
using Taran.Shared.Core.Exceptions;

namespace SabaRayane.Contract.Core.Aggregates.ProductAggregate;

public class Product : AggregateRoot<int>
{
    [StringLength(50)]
    public string Name { get; private set; }

    public long Price { get; private set; }

    [StringLength(500)]
    public string? Description { get; private set; }

    public Product(int creatorUserId, string name, long price, string description) : base(creatorUserId)
    {
        Name = DomainArgumentNullOrEmptyException.Ensure(name, nameof(Name));
        Price = price > 0 ? price : throw new DomainInvalidArgumentException(nameof(Price));
        Description = description;
    }

    public void Update(int userId, string name, long price, string? description)
    {
        Name = DomainArgumentNullOrEmptyException.Ensure(name, nameof(Name));
        Price = price > 0 ? price : throw new DomainInvalidArgumentException(nameof(Price));
        Description = description;

        Update(userId);
    }
}