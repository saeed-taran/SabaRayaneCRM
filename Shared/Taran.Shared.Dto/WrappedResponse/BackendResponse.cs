namespace Taran.Shared.Dtos.WrappedResponse;

public record BackendResponse<Response>
{
    public bool Success { get; private set; }
    public string? ErrorMessage { get; private set; }
    public Response? Data { get; private set; }

    protected BackendResponse(Response data)
    {
        Success = true;
        Data = data;
    }

    protected BackendResponse(string errorMessage)
    {
        Success = false;
        ErrorMessage = errorMessage;
    }

    public BackendResponse(bool success, string? errorMessage, Response? data)
    {
        Success = success;
        ErrorMessage = errorMessage;
        Data = data;
    }

    public void SetErrorMessage(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
