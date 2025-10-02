namespace Taran.Shared.Dtos.Dtos.IO;

public class GetImportTemplateResponseDto
{
    public Stream ResultStream { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }

    public GetImportTemplateResponseDto(Stream resultStream, string contentType, string fileName)
    {
        ResultStream = resultStream;
        ContentType = contentType;
        FileName = fileName;
    }
}
