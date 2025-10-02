using Taran.Shared.Dtos.Dtos.IO;
using Taran.Shared.Application.Queries;
using Taran.Shared.Dtos;

namespace Taran.Shared.Application.Queries.IO;

public record GetImportTemplateQuery : RequestWithUserDtoBase, IQueryWithUser<GetImportTemplateResponseDto>
{
    public Type DtoTypeToImport { get; set; }

    public Dictionary<string, object> FixedValues { get; set; } = new();

    public GetImportTemplateQuery(Type dtoTypeToImport)
    {
        DtoTypeToImport = dtoTypeToImport ?? throw new ArgumentNullException("dtoTypeToImport");
    }
}
