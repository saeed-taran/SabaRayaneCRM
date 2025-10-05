using MediatR;
using SabaRayane.Contract.Application.Queries.MessageTemplates.PlaceHolders;
using SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;
using SabaRayane.Contract.Dtos.MessageTemplates.PlaceHolders;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.QueryHandlers.MessageTemplates.PlaceHolders;

public class SearchPlaceHolderQueryHandler : IRequestHandler<SearchPlaceHolderQuery, PaginatedResponseDto<SearchPlaceHolderResponseDto>>
{
    public Task<PaginatedResponseDto<SearchPlaceHolderResponseDto>> Handle(SearchPlaceHolderQuery request, CancellationToken cancellationToken)
    {
        var dtos = PlaceHolder.PlaceHolders
            .Select(kvp => new SearchPlaceHolderResponseDto(kvp.Value.Name, kvp.Value.Title)).ToList();

        return Task.FromResult(new PaginatedResponseDto<SearchPlaceHolderResponseDto>(request.Skip, request.Take, PlaceHolder.PlaceHolders.Count, dtos));
    }
}
