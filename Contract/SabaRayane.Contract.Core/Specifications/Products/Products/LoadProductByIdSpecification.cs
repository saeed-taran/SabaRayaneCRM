using SabaRayane.Contract.Core.Aggregates.ProductAggregate;
using Taran.Shared.Core.Specifications;

namespace SabaRayane.Contract.Core.Specifications.s.Products;
public class LoadProductByIdSpecification : SpecificationBase<Product>
{
    public LoadProductByIdSpecification(int id) : base(c => c.Id == id)
    {
    }
}
