using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Queries.Customers.Agreements;
using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using SabaRayane.Contract.Core.Specifications.Customers.Agreements;
using SabaRayane.Contract.Dtos.Customers.Agreements;
using Taran.Shared.Core.Repository;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.Services.Calendar;

namespace SabaRayane.Contract.Application.QueryHandlers.Customers.Agreements;

public class SearchAgreementQueryHandler : IRequestHandler<SearchAgreementQuery, PaginatedResponseDto<SearchAgreementResponseDto>>
{
    private readonly IGenericReadRepository<Agreement, int> agreementReadRepository;
    private readonly ICalendar calendar;

    public SearchAgreementQueryHandler(IGenericReadRepository<Agreement, int> agreementReadRepository, ICalendar calendar)
    {
        this.agreementReadRepository = agreementReadRepository;
        this.calendar = calendar;
    }

    public async Task<PaginatedResponseDto<SearchAgreementResponseDto>> Handle(SearchAgreementQuery request, CancellationToken cancellationToken)
     {
        
        var specification = new SearchAgreementSpecification(request.Skip, request.Take, request.CustomerId);
        var agreements = await agreementReadRepository.FindWithSpecification(specification).ToListAsync(cancellationToken);
        
        specification.SetIgnorePagination(true);
        var totalCount = await agreementReadRepository.FindWithSpecification(specification).CountAsync(cancellationToken);

        var agreementDtos = agreements.Select(c => new SearchAgreementResponseDto(
            c.Id, 
            c.CustomerId, 
            c.Customer.FullName,
            c.ProductId, 
            c.Product.Name,
            c.Price,
            calendar.ConvertDate(c.AgreementDate), 
            c.DurationInMonths, 
            c.ExtraUsersCount
        )).ToList();

        return new(request.Skip, request.Take, totalCount, agreementDtos);
        
     }

}
