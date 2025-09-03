using Taran.Shared.Dtos.WrappedResponse;

namespace Taran.Shared.UI.Services.HttpService
{
    public interface IHttpService
    {
        Task<BackendResponse<Response>> Get<Request, Response>(string url, Request request);
        Task<BackendResponse<Response>> Get<Response>(string url);
        Task<BackendResponse<Response>> Post<Request, Response>(string url, Request request);
        Task<BackendResponse<Response>> Post<Response>(string url);
        Task<BackendResponse<Response>> Put<Request, Response>(string url, Request request);
        Task<BackendResponse<Response>> Delete<Response>(string url);
    }
}