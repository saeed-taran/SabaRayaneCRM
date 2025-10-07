using Taran.Shared.Dtos.Dtos.IO;
using Taran.Shared.Dtos;
using Taran.Shared.Core.Exceptions;

namespace Taran.Shared.Application.Queries.IO;

public record GetImportTemplateQuery : RequestWithUserDtoBase, IQueryWithUser<GetImportTemplateResponseDto>
{
    public Type DtoTypeToImport { get; set; }

    public Dictionary<string, object> FixedValues { get; set; } = new();

    public string FileNameToSave { get; set; }

    public GetImportTemplateQuery(Type dtoTypeToImport, string fileNameToSave)
    {
        DtoTypeToImport = dtoTypeToImport ?? throw new ArgumentNullException("dtoTypeToImport");
        FileNameToSave = DomainArgumentNullOrEmptyException.Ensure(fileNameToSave, nameof(FileNameToSave));
    }
}
