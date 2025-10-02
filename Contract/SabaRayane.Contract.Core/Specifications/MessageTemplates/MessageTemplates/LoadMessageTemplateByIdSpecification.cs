using SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;
using Taran.Shared.Core.Specifications;

namespace SabaRayane.Contract.Core.Specifications.s.MessageTemplates;
public class LoadMessageTemplateByIdSpecification : SpecificationBase<MessageTemplate>
{
    public LoadMessageTemplateByIdSpecification(int id) : base(c => c.Id == id)
    {
    }
}
