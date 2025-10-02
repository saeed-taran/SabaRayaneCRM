namespace Taran.Shared.Dtos.Dtos.IO;

public record ImportDataResponseDto
{
    public int Done { get; set; }
    public int Error { get; set; }

    public List<ImportDataErrorDto> ErrorMessages { get; set; } = new();

    public ImportDataResponseDto(int done, int error, List<ImportDataErrorDto> errorMessages)
    {
        Done = done;
        Error = error;
        ErrorMessages = errorMessages;
    }
}

public record ImportDataErrorDto 
{
    public int RowNumber { get; set; }
    public string? FieldName { get; set; }
    public string? Error {  get; set; }

    public ImportDataErrorDto(int rowNumber, string? fieldName, string? error)
    {
        RowNumber = rowNumber;
        FieldName = fieldName;
        Error = error;
    }
}