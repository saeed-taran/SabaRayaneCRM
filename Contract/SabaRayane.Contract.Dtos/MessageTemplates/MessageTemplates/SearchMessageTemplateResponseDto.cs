namespace SabaRayane.Contract.Dtos.s.MessageTemplates;

public record SearchMessageTemplateResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Message { get; set; }
    public int DaysUntilAgreementExpire { get; set; }

    public SearchMessageTemplateResponseDto(int id, string name, string message, int daysUntilAgreementExpire)
    {
        Id = id;
        Name = name;
        Message = message;
        DaysUntilAgreementExpire = daysUntilAgreementExpire;
    }
}
