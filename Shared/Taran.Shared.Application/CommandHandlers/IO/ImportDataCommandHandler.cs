using MediatR;
using OfficeOpenXml;
using SabaRayane.Notification.BackendService.Notifications;
using Taran.Shared.Application.Commands.IO;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Dtos.Dtos.IO;
using Taran.Shared.Dtos.Languages;
using System.ComponentModel.DataAnnotations;
using Taran.Shared.Application.Commands;

namespace Taran.Shared.Application.CommandHandlers.IO;

public class ImportDataCommandHandler<CreateCommandType> : IRequestHandler<ImportDataCommand<CreateCommandType>, ImportDataResponseDto> where CreateCommandType : ICommandWithUser<bool>
{
    private readonly INotificationService notificationService;

    public ImportDataCommandHandler(INotificationService notificationService)
    {
        this.notificationService = notificationService;
    }

    public async Task<ImportDataResponseDto> Handle(ImportDataCommand<CreateCommandType> request, CancellationToken cancellationToken)
    {
        ExcelPackage.License.SetNonCommercialPersonal("SaeedTaran");

        int done = 0;
        int error = 0;

        var lastNotifTime = DateTime.MinValue;

        List<ImportDataErrorDto> Errors = new();

        using (var stream = new MemoryStream())
        {
            request.FileStream.CopyTo(stream);
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                if (rowCount == 1)
                    throw new DomainInvalidOperationException(nameof(KeyWords.UploadedFileContainsNoRows));

                if (colCount == 0)
                    throw new DomainInvalidOperationException(nameof(KeyWords.UploadedFileContainsNoColumns));

                var headers = new List<string>();
                for (int col = 1; col <= colCount; col++)
                {
                    var headerText = worksheet.Cells[1, col]?.Comment?.Text;
                    if(string.IsNullOrEmpty(headerText))
                        throw new DomainInvalidOperationException(nameof(KeyWords.CantRetrieveColumnHeaderName));
                    headers.Add(headerText);
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    if (request.NotifyProgress)
                    {
                        var donePercentage = (100 * done) / (rowCount - 1);
                        var errorPercentage = (100 * error) / (rowCount - 1);
                        if (DateTime.Now.Subtract(lastNotifTime).TotalMilliseconds >= request.SendNotifEveryMilliSeconds)
                        {
                            lastNotifTime = DateTime.Now;
                            await notificationService.SendTaskStatus(request.GetUserId(), new(request.ImportTaskName, donePercentage, errorPercentage));
                        }
                    }

                    var rowData = new Dictionary<string, object?>();
                    for (int col = 1; col <= colCount; col++)
                    {
                        var value = worksheet.Cells[row, col].Text;
                        rowData[headers[col - 1]] = string.IsNullOrWhiteSpace(value) ? null : value;
                    }

                    CreateCommandType createCommand;

                    try
                    {
                        createCommand = request.CreateCommandGenerator(rowData);
                        var validateResult = ValidateObject(createCommand);
                        foreach (var result in validateResult) 
                        {
                            if(Errors.Count < request.ErrorListMaxLenght)
                                Errors.Add(new(row, result.MemberNames.First(), result.ErrorMessage));
                        }

                        if (validateResult.Any())
                        {
                            error++;
                            continue;
                        }
                    }
                    catch
                    {
                        error++;
                        continue;
                    }

                    try
                    {
                        await request.CreateCommandExecuter(createCommand);
                        done++;
                    }
                    catch(Exception ex)
                    {
                        var fieldName = (ex as DomainEntityImportException)?.FieldName;
                        if (Errors.Count < request.ErrorListMaxLenght)
                            Errors.Add(new(row, fieldName, ex.Message));

                        error++;
                        continue;
                    }
                }

                if (request.NotifyProgress)
                {
                    var donePercentage = (100 * done) / (rowCount - 1);
                    var errorPercentage = (100 * error) / (rowCount - 1);
                    await notificationService.SendTaskStatus(request.GetUserId(), new(request.ImportTaskName, donePercentage, errorPercentage));
                }
            }
        }

        return new(done, error, Errors);
    }

    private List<ValidationResult> ValidateObject(object Obj)
    {
        var ctx = new ValidationContext(Obj);
        List<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(Obj, ctx, validationResults, true);
        return validationResults;
    }
}