using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using Taran.Shared.Core.Specifications;

namespace SabaRayane.Contract.Core.Specifications.s.Customers;
public class SearchCustomerSpecification : SpecificationBase<Customer>
{
    public SearchCustomerSpecification(int skip, int take) : base(e => true)
    {
        SetPagination(skip, take);
    }
}
