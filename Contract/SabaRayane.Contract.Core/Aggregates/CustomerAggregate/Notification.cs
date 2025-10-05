using SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;
using System.ComponentModel.DataAnnotations;
using Taran.Shared.Core.Entity;

namespace SabaRayane.Contract.Core.Aggregates.CustomerAggregate;

public class Notification : BaseEntity<long>
{
    public int AgreementId { get; private set; }
    public Agreement Agreement { get; private set; }

    public int MessageTemplateId { get; private set; }
    public MessageTemplate MessageTemplate { get; private set; }

    [StringLength(500)]
    public string Message {  get; private set; }

    public NotificationStatus NotificationStatus { get; private set; }

    public DateTime LastTryDate { get; private set; }
    public int TryCount { get; private set; }

    [StringLength(250)]
    public string? ErrorDescription { get; private set; }

    internal Notification(int creatorUserId, int agreementId, int messageTemplateId, string message) : base(creatorUserId)
    {
        AgreementId = agreementId;
        MessageTemplateId = messageTemplateId;
        Message = message;

        NotificationStatus = NotificationStatus.None;
    }

    internal void Sent()
    {
        NotificationStatus = NotificationStatus.Sent;
    }

    internal void Faild(string errorDescription)
    {
        NotificationStatus = NotificationStatus.Faild;
        ErrorDescription = errorDescription;
    }

    internal void Try() 
    {
        LastTryDate = DateTime.Now;
        TryCount++;
    }
}
