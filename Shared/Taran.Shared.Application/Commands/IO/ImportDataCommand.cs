
using Taran.Shared.Dtos.Dtos.IO;
using Taran.Shared.Application.Commands;
using Taran.Shared.Dtos;

namespace Taran.Shared.Application.Commands.IO;

public record ImportDataCommand<CreateCommandType> : RequestWithUserDtoBase, ICommandWithUser<ImportDataResponseDto>
{
    public Stream FileStream { get; set; }
    public Func<Dictionary<string, object?>, CreateCommandType> CreateCommandGenerator;
    public Func<CreateCommandType, Task> CreateCommandExecuter;
    public string ImportTaskName { get; set; }
    public bool NotifyProgress { get; set; }
    public int SendNotifEveryMilliSeconds { get; set; }
    public int ErrorListMaxLenght { get; set; }

    public ImportDataCommand(string importTaskName, bool notifyProgress, Stream fileStream, Func<Dictionary<string, object?>, CreateCommandType> createCommandGenerator, Func<CreateCommandType, Task> createCommandExecuter, int sendNotifEveryMilliSeconds, int errorListMaxLenght)
    {
        ImportTaskName = importTaskName;
        NotifyProgress = notifyProgress;
        FileStream = fileStream;
        CreateCommandGenerator = createCommandGenerator;
        CreateCommandExecuter = createCommandExecuter;
        SendNotifEveryMilliSeconds = sendNotifEveryMilliSeconds;
        ErrorListMaxLenght = errorListMaxLenght;
    }
}
