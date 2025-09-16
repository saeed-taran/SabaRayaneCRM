using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Queries.s.Products;
using SabaRayane.Contract.Core.Aggregates.ProductAggregate;
using SabaRayane.Contract.Core.Specifications.s.Products;
using SabaRayane.Contract.Dtos.s.Products;
using Taran.Shared.Core.Repository;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.QueryHandlers.s.Products;

public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, PaginatedResponseDto<SearchProductResponseDto>>
{
    private readonly IGenericReadRepository<Product, int> productReadRepository;

    public SearchProductQueryHandler(IGenericReadRepository<Product, int> productReadRepository)
    {
        this.productReadRepository = productReadRepository;
    }

    public async Task<PaginatedResponseDto<SearchProductResponseDto>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
    {

        var specification = new SearchProductSpecification(request.Skip, request.Take);
        var products = await productReadRepository.FindWithSpecification(specification).ToListAsync(cancellationToken);

        specification.SetIgnorePagination(true);
        var totalCount = await productReadRepository.FindWithSpecification(specification).CountAsync(cancellationToken);

        var productDtos = products.Select(c => new SearchProductResponseDto(
            c.Id,
            c.Name,
            c.Price,
            c.Description
        )).ToList();

        return new(request.Skip, request.Take, totalCount, productDtos);
    }
}