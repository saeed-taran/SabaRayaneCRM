using System.ComponentModel.DataAnnotations;
using Taran.Shared.Core.Entity;
using Taran.Shared.Core.Exceptions;

namespace SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;

public class MessageTemplate : AggregateRoot<int>
{
    [StringLength(50)]
    public string Name { get; private set; }
    
    [StringLength(500)]
    public string Message { get; private set; }
    public int DaysUntilAgreementExpire { get; private set; }

    public MessageTemplate(int creatorUserId, string name, string message, int daysUntilAgreementExpire) : base(creatorUserId)
    {
        Name = DomainArgumentNullOrEmptyException.Ensure(name, nameof(Name));
        Message = DomainArgumentNullOrEmptyException.Ensure(message, nameof(Message)); ;
        DaysUntilAgreementExpire = daysUntilAgreementExpire;
    }

    public void Update(int userId, string name, string message, int daysUntilAgreementExpire)
    {
        Name = DomainArgumentNullOrEmptyException.Ensure(name, nameof(Name));
        Message = DomainArgumentNullOrEmptyException.Ensure(message, nameof(Message)); ;
        DaysUntilAgreementExpire = daysUntilAgreementExpire;

        Update(userId);
    }
}
