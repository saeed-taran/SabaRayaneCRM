namespace Taran.Shared.Dtos.WrappedResponse;

public record SuccessfulResponse<Response> : BackendResponse<Response>
{
    public SuccessfulResponse(Response response) : base(response)
    {
        
    }
}
