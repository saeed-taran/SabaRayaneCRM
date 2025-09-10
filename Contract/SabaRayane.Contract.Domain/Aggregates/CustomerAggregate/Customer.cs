using SabaRayane.Contract.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using Taran.Shared.Core.Entity;
using Taran.Shared.Core.Exceptions;

namespace SabaRayane.Contract.Domain.Aggregates.CustomerAggregate;

public class Customer : AggregateRoot<int>
{
    [StringLength(50)]
    public string CustomerId { get; private set; }

    [StringLength(50)]
    public string FirstName { get; private set; }

    [StringLength(50)]
    public string LastName { get; private set; }

    [StringLength(50)]
    public string? StoreName { get; private set; }

    public MobileNumber? MobileNumber { get; private set; }

    [StringLength(500)]
    public string? Description { get; private set; }

    private List<Contract> contracts = [];
    public IReadOnlyCollection<Contract> Contracts => contracts;

    public Customer(int creatorUserId, string customerId, string firstName, string lastName, string? storeName, MobileNumber? mobileNumber, string? description)
        : base(creatorUserId)
    {
        CustomerId = DomainArgumentNullOrEmptyException.Ensure(customerId, nameof(CustomerId));
        FirstName = DomainArgumentNullOrEmptyException.Ensure(firstName, nameof(FirstName));
        LastName = DomainArgumentNullOrEmptyException.Ensure(lastName, nameof(LastName));
        StoreName = storeName;
        MobileNumber = mobileNumber;
        Description = description;
    }

    public void AddContract(int creatorUserId, int productId, long price, DateOnly contractDate, int durationInMonths, int extraUsersCount) 
    {
        if (contracts.Exists(c => c.ContractDate >= contractDate))
            throw new DomainInvalidOperationException(nameof(Contract.ContractDate));

        Contract contract = new(creatorUserId, productId, price, contractDate, durationInMonths, extraUsersCount);
        contracts.Add(contract);
    }
}
