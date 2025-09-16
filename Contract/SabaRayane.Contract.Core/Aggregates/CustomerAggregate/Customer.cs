using SabaRayane.Contract.Core.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taran.Shared.Core.Entity;
using Taran.Shared.Core.Exceptions;

namespace SabaRayane.Contract.Core.Aggregates.CustomerAggregate;

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

    private List<Agreement> agreements = [];
    public IReadOnlyCollection<Agreement> Agreements => agreements;

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";

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

    public void Update(int userId, string customerId, string firstName, string lastName, string? storeName, MobileNumber? mobileNumber, string? description)
    {
        CustomerId = DomainArgumentNullOrEmptyException.Ensure(customerId, nameof(CustomerId));
        FirstName = DomainArgumentNullOrEmptyException.Ensure(firstName, nameof(FirstName));
        LastName = DomainArgumentNullOrEmptyException.Ensure(lastName, nameof(LastName));
        StoreName = storeName;
        MobileNumber = mobileNumber;
        Description = description;

        Update(userId);
    }

    public void AddAgreement(int creatorUserId, int productId, long price, DateOnly agreementDate, int durationInMonths, int extraUsersCount) 
    {
        if (agreements.Exists(c => c.AgreementDate >= agreementDate))
            throw new DomainInvalidOperationException(nameof(Agreement.AgreementDate));

        Agreement agreement = new(creatorUserId, productId, price, agreementDate, durationInMonths, extraUsersCount);
        agreements.Add(agreement);
    }

    public void UpdateAgreement(int userId, int agreementId, int productId, DateOnly agreementDate, int durationInMonths, int extraUsersCount)
    {
        var nextAgreement = agreements.Where(a => a.Id > agreementId).OrderBy(a => a.AgreementDate).FirstOrDefault();
        var previousAgreement = agreements.Where(a => a.Id < agreementId).OrderByDescending(a => a.AgreementDate).FirstOrDefault();

        if (nextAgreement is not null && nextAgreement.AgreementDate <= agreementDate)
            throw new DomainInvalidOperationException(nameof(Agreement.AgreementDate));

        if (previousAgreement is not null && previousAgreement.AgreementDate >= agreementDate)
            throw new DomainInvalidOperationException(nameof(Agreement.AgreementDate));

        var agreement = agreements.FirstOrDefault(a => a.Id == agreementId)
            ?? throw new DomainEntityNotFoundException(nameof(Agreement));

        agreement.Update(userId, productId, agreementDate, durationInMonths, extraUsersCount);
    }

    public void DeleteAgreement(int agreementId)
    {
        var agreement = agreements.FirstOrDefault(a => a.Id == agreementId)
            ?? throw new DomainEntityNotFoundException(nameof(Agreement));

        agreements.Remove(agreement);
    }
}
