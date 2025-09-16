using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using Taran.Shared.Core.Specifications;

namespace SabaRayane.Contract.Core.Specifications.Customers.Agreements;
public class SearchAgreementSpecification : SpecificationBase<Agreement>
{
    public SearchAgreementSpecification(int skip, int take) : base(e => true)
    {
        SetPagination(skip, take);

        AddInclude(a => a.Include(a => a.Customer));
        AddInclude(a => a.Include(a => a.Product));
    }
}
