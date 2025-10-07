using Microsoft.EntityFrameworkCore;
using Taran.Shared.Core.Specifications;
using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;

namespace SabaRayane.Contract.Core.Specifications.Customers.CustomerAggregate;
public class SearchNotificationSpecification : SpecificationBase<Notification>
{
    public SearchNotificationSpecification(int skip, int take) : base(e => true)
    {
        SetPagination(skip, take);
        AddInclude(q => q.Include(c => c.MessageTemplate));
    }
}
