namespace Taran.Shared.Dtos.WrappedResponse;

public record ErrorResponse : BackendResponse<byte?>
{
    public ErrorResponse(string errorMessage) : base(errorMessage)
    {

    }
}