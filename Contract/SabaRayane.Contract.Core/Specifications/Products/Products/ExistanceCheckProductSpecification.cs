using SabaRayane.Contract.Core.Aggregates.ProductAggregate;
using Taran.Shared.Core.Specifications;

namespace SabaRayane.Contract.Core.Specifications.Products.Products;

public class ExistanceCheckProductSpecification : SpecificationBase<Product>
{
    public ExistanceCheckProductSpecification(string name) 
        : base(p => p.Name == name)
    {
    }
}
