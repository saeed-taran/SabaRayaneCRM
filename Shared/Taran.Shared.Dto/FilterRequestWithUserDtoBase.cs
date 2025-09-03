using Taran.Shared.Dtos;

namespace Taran.Shared.Dtos;

public record FilterRequestWithUserDtoBase : RequestWithUserDtoBase
{
    public int Skip { get; set; }
    public int Take { get; set; }
}
