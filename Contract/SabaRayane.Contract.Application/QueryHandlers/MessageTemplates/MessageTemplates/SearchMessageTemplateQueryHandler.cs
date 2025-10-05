using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Queries.s.MessageTemplates;
using SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;
using SabaRayane.Contract.Core.Specifications.s.MessageTemplates;
using SabaRayane.Contract.Dtos.s.MessageTemplates;
using Taran.Shared.Core.Repository;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.QueryHandlers.s.MessageTemplates;

public class SearchMessageTemplateQueryHandler : IRequestHandler<SearchMessageTemplateQuery, PaginatedResponseDto<SearchMessageTemplateResponseDto>>
{
    private readonly IGenericReadRepository<MessageTemplate, int> messageTemplateReadRepository;

    public SearchMessageTemplateQueryHandler(IGenericReadRepository<MessageTemplate, int> messageTemplateReadRepository)
    {
        this.messageTemplateReadRepository = messageTemplateReadRepository;
    }

    public async Task<PaginatedResponseDto<SearchMessageTemplateResponseDto>> Handle(SearchMessageTemplateQuery request, CancellationToken cancellationToken)
    {

        var specification = new SearchMessageTemplateSpecification(request.Skip, request.Take);
        var messageTemplates = await messageTemplateReadRepository.FindWithSpecification(specification).ToListAsync(cancellationToken);

        specification.SetIgnorePagination(true);
        var totalCount = await messageTemplateReadRepository.FindWithSpecification(specification).CountAsync(cancellationToken);

        var messageTemplateDtos = messageTemplates.Select(c => new SearchMessageTemplateResponseDto(
            c.Id,
            c.Name,
            c.Template,
            c.DaysUntilAgreementExpire
        )).ToList();

        return new(request.Skip, request.Take, totalCount, messageTemplateDtos);

    }

}
