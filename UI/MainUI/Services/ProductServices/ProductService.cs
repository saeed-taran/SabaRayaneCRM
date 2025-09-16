using SabaRayane.Contract.Dtos.s.Products;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.WrappedResponse;
using Taran.Shared.UI.ConfigurationModels;
using Taran.Shared.UI.Services;
using Taran.Shared.UI.Services.HttpService;
using Taran.UI.Main.Services.CurrencyServices;

namespace Taran.UI.Main.Services.ProductServices;

public class ProductService : ServiceBase, IProductService
{
    public ProductService(NavigationConfiguration navConfig, IHttpService httpService, IToastService toastService)
        : base(toastService, navConfig.ContractApi + "/product", httpService)
    {
    }


    #region Product services
    public async Task<BackendResponse<PaginatedResponseDto<SearchProductResponseDto>>> SearchProduct(SearchProductRequestDto searchRequest)
    {
        return await httpService.Get<SearchProductRequestDto, PaginatedResponseDto<SearchProductResponseDto>>(
            baseUrl,
            searchRequest
        );
    }

    public async Task<BackendResponse<bool?>> CreateProduct(CreateProductRequestDto createRequestDto)
    {
        return await httpService.Post<CreateProductRequestDto, bool?>(
            baseUrl,
            createRequestDto
        );
    }

    public async Task<BackendResponse<bool?>> UpdateProduct(UpdateProductRequestDto updateRequestDto)
    {
        return await httpService.Put<UpdateProductRequestDto, bool?>(
            baseUrl,
            updateRequestDto
        );
    }

    public async Task<BackendResponse<bool?>> DeleteProduct(int id)
    {
        return await httpService.Delete<bool?>(
            baseUrl + $"/{id}"
        );
    }

    public async Task<List<(int, string)>> GetProductDropDownItems(int skip, int take, string term)
    {
        var products = await SearchProduct(new SearchProductRequestDto { Skip = skip, Take = take });
        return _GetDropDownItems(products, c => (c.Id, c.Name));
    }
    #endregion

}
