using SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;
using Taran.Shared.Core.Specifications;

namespace SabaRayane.Contract.Core.Specifications.MessageTemplates.MessageTemplates;

public class ExistanceCheckMessageTemplateSpecification : SpecificationBase<MessageTemplate>
{
    public ExistanceCheckMessageTemplateSpecification(int daysUntilAgreementExpire) 
        : base(m => m.DaysUntilAgreementExpire == daysUntilAgreementExpire)
    {
    }
}
