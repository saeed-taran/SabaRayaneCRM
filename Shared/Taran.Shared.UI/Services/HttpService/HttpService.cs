using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Newtonsoft.Json;
using Taran.Shared.Dtos.Languages;
using Taran.Shared.Languages;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;
using Taran.Shared.Dtos.WrappedResponse;
using Taran.Shared.UI.ConfigurationModels;

namespace Taran.Shared.UI.Services.HttpService;

public class HttpService : IHttpService
{
    private readonly HttpClient _httpClient;
    private readonly ITranslator _translator;
    private readonly NavigationManager _navigationManager;
    private readonly NavigationConfiguration _navConfig;

    public HttpService(ITranslator translator, NavigationManager navigationManager, NavigationConfiguration navConfig, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _translator = translator;
        _navigationManager = navigationManager;
        _navConfig = navConfig;
    }

    public async Task<BackendResponse<Response>> Get<Request, Response>(string url, Request request)
    {
        url = url + (request is null ? "" : "?" + ObjectToQueryString(request));
        return await Send<Request, Response>(url, default, HttpMethod.Get);
    }

    public async Task<BackendResponse<Response>> Get<Response>(string url)
    {
        return await Send<object?, Response>(url, null, HttpMethod.Get);
    }

    public async Task<BackendResponse<Response>> Post<Response>(string url)
    {
        return await Send<object?, Response>(url, null, HttpMethod.Post);
    }

    public async Task<BackendResponse<Response>> Post<Request, Response>(string url, Request request)
    {
        return await Send<Request, Response>(url, request, HttpMethod.Post);
    }

    public async Task<BackendResponse<Response>> Put<Request, Response>(string url, Request request)
    {
        return await Send<Request, Response>(url, request, HttpMethod.Put);
    }

    public async Task<BackendResponse<Response>> Delete<Response>(string url)
    {
        return await Send<object?, Response>(url, null, HttpMethod.Delete);
    }

    private async Task<BackendResponse<Response>> Send<Request, Response>(string url, Request? request, HttpMethod httpMethod)
    {
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, url);
        
        if(request is not null)
            httpRequestMessage.Content = JsonContent.Create(request);

        httpRequestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        httpRequestMessage.Headers.Add("X-Requested-With", ["XMLHttpRequest"]);

        try
        {
            var response = await _httpClient.SendAsync(httpRequestMessage);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) 
            {
                _navigationManager.NavigateTo(_navConfig.UI + "/login", true);
                return new BackendResponse<Response>(false, "", default);
            }
            var backendResponse = await response.Content.ReadFromJsonAsync<BackendResponse<Response>>();
            if (backendResponse is null)
                throw new Exception("Backend response was null!!");
            if (!backendResponse.Success)
                backendResponse.SetErrorMessage(_translator.Translate(backendResponse.ErrorMessage ?? nameof(KeyWords.UnknownError)));
            return backendResponse;
        }
        catch (HttpRequestException e) 
        {
            //_navigationManager.NavigateTo(_navConfig.UI + "/login", true);
            return new BackendResponse<Response>(false, "", default);
        }
    }

    public async Task<BackendResponse<Response>> UploadFile<Response>(string url, IBrowserFile browserFile)
    {
        var fileContent = new MultipartFormDataContent();

        MemoryStream memoryStream = new();
        await browserFile.OpenReadStream().CopyToAsync(memoryStream);

        var dataContent = new ByteArrayContent(memoryStream.GetBuffer());
        dataContent.Headers.ContentType = MediaTypeHeaderValue.Parse(browserFile.ContentType);

        fileContent.Add(dataContent, "file", browserFile.Name);

        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url);
        httpRequestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        httpRequestMessage.Headers.Add("X-Requested-With", ["XMLHttpRequest"]);
        httpRequestMessage.Content = fileContent;

        var response = await _httpClient.SendAsync(httpRequestMessage);
        var backendResponse = await response.Content.ReadFromJsonAsync<BackendResponse<Response>>();
        if (backendResponse is null)
            throw new Exception("Backend response was null!!");
        if (!backendResponse.Success)
            backendResponse.SetErrorMessage(_translator.Translate(backendResponse.ErrorMessage ?? nameof(KeyWords.UnknownError)));
        return backendResponse;
    }

    private string ObjectToQueryString(object obj)
    {
        var step1 = JsonConvert.SerializeObject(obj);

        var step2 = JsonConvert.DeserializeObject<IDictionary<string, string>>(step1);

        var step3 = step2.Select(x => HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value));

        return string.Join("&", step3);
    }
}
