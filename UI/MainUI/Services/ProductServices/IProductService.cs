using SabaRayane.Contract.Dtos.s.Products;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.WrappedResponse;

namespace Taran.UI.Main.Services.ProductServices
{
    public interface IProductService
    {
        Task<BackendResponse<bool?>> CreateProduct(CreateProductRequestDto createRequestDto);
        Task<BackendResponse<bool?>> DeleteProduct(int id);
        Task<List<(int, string)>> GetProductDropDownItems(int skip, int take, string term);
        Task<BackendResponse<PaginatedResponseDto<SearchProductResponseDto>>> SearchProduct(SearchProductRequestDto searchRequest);
        Task<BackendResponse<bool?>> UpdateProduct(UpdateProductRequestDto updateRequestDto);
    }
}