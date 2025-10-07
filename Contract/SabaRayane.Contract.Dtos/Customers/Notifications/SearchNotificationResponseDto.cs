namespace SabaRayane.Contract.Dtos.Customers.CustomerAggregate;

public record SearchNotificationResponseDto
{
    public long Id { get; set; }
    public int AgreementId { get; set; }

    public int MessageTemplateId { get; set; }
    public string MessageTemplateTitle { get; set; }
    public string Message { get; set; }
    public string NotificationStatusTitle { get; set; }
    public string LastTryDate { get; set; }
    public int TryCount { get; set; }
    public string? ErrorDescription { get; set; }

    public SearchNotificationResponseDto(long id, int agreementId, int messageTemplateId, string messageTemplateTitle, string message, string notificationStatusTitle, string lastTryDate, int tryCount, string? errorDescription)
    {
        Id = id;
        AgreementId = agreementId;
        MessageTemplateId = messageTemplateId;
        MessageTemplateTitle = messageTemplateTitle;
        Message = message;
        NotificationStatusTitle = notificationStatusTitle;
        LastTryDate = lastTryDate;
        TryCount = tryCount;
        ErrorDescription = errorDescription;
    }
}
