using SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;
using Taran.Shared.Core.Specifications;

namespace SabaRayane.Contract.Core.Specifications.s.MessageTemplates;
public class SearchMessageTemplateSpecification : SpecificationBase<MessageTemplate>
{
    public SearchMessageTemplateSpecification(int skip, int take) : base(e => true)
    {
        SetPagination(skip, take);
    }
}
