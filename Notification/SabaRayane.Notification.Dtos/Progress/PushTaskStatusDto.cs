namespace SabaRayane.Notification.Dtos.Progress;

public record PushTaskStatusDto
{
    public string TaskName { get; set; }
    public int DonePercentage { get; set; }
    public int ErrorPercentage { get; set; }

    public int TotalPercentage => DonePercentage + ErrorPercentage;
    public bool IsDone => DonePercentage + ErrorPercentage >= 100;

    public PushTaskStatusDto(string taskName, int donePercentage, int errorPercentage)
    {
        TaskName = taskName;
        DonePercentage = donePercentage;
        ErrorPercentage = errorPercentage;
    }

    public PushTaskStatusDto()
    {
    }
}
