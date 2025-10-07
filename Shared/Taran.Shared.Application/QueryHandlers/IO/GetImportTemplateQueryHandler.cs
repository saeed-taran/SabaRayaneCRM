using MediatR;
using Taran.Shared.Application.Queries.IO;
using Taran.Shared.Application.Services.IO;
using Taran.Shared.Dtos.Dtos.IO;

namespace Taran.Shared.Application.CommandHandlers.IO;

public class GetImportTemplateQueryHandler : IRequestHandler<GetImportTemplateQuery, GetImportTemplateResponseDto>
{
    private readonly IExcelService excelService;

    public GetImportTemplateQueryHandler(IExcelService excelService)
    {
        this.excelService = excelService;
    }

    public async Task<GetImportTemplateResponseDto> Handle(GetImportTemplateQuery request, CancellationToken cancellationToken)
    {
        var fileBytes = excelService.GenerateExcelWithValidation(request.DtoTypeToImport, request.FixedValues);

        return new GetImportTemplateResponseDto(new MemoryStream(fileBytes), "application/octet-stream", $"{request.FileNameToSave}.xlsx");
    }
}
