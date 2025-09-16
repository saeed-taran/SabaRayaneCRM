using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Taran.Shared.Core.Specifications;

namespace SabaRayane.Contract.Core.Specifications.s.Customers;
public class LoadCustomerByIdSpecification : SpecificationBase<Customer>
{
    public LoadCustomerByIdSpecification(int id) : base(c => c.Id == id)
    {
        AddInclude(q => q.Include(c => c.Agreements));
    }
}
