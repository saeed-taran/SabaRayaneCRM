using SabaRayane.Contract.Core.Aggregates.ProductAggregate;
using Taran.Shared.Core.Specifications;

namespace SabaRayane.Contract.Core.Specifications.s.Products;
public class SearchProductSpecification : SpecificationBase<Product>
{
    public SearchProductSpecification(int skip, int take) : base(e => true)
    {
        SetPagination(skip, take);
    }
}
