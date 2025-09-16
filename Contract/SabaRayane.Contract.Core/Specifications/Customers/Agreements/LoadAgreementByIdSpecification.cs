using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using Taran.Shared.Core.Specifications;

namespace SabaRayane.Contract.Core.Specifications.Customers.Agreements;
public class LoadAgreementByIdSpecification : SpecificationBase<Agreement>
{
    public LoadAgreementByIdSpecification(int id) : base(c => c.Id == id)
    {
    }
}
