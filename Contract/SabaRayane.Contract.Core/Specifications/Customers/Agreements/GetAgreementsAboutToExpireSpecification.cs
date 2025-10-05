using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using Taran.Shared.Core.Specifications;

namespace SabaRayane.Contract.Core.Specifications.Customers.Agreements;

public class GetAgreementsAboutToExpireSpecification : SpecificationBase<Agreement>
{
    public GetAgreementsAboutToExpireSpecification(int skip, int take, int daysUntilExpire, int messageTemplateId)
        : base(
            g => g.ExpireDate == DateOnly.FromDateTime(DateTime.Now.AddDays(daysUntilExpire)) &&
                !g.Notifications.Any(n => n.MessageTemplateId == messageTemplateId && n.CreationDate >= DateTime.Today)
        )
    {
        SetPagination(skip, take);
    }
}
