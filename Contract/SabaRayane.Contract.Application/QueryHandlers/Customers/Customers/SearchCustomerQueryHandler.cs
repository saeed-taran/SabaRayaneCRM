using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Queries.s.Customers;
using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using SabaRayane.Contract.Core.Specifications.s.Customers;
using SabaRayane.Contract.Dtos.s.Customers;
using Taran.Shared.Core.Repository;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.QueryHandlers.s.Customers;

public class SearchCustomerQueryHandler : IRequestHandler<SearchCustomerQuery, PaginatedResponseDto<SearchCustomerResponseDto>>
{
    private readonly IGenericReadRepository<Customer, int> customerReadRepository;

    public SearchCustomerQueryHandler (IGenericReadRepository<Customer, int> customerReadRepository)
     {
      this.customerReadRepository = customerReadRepository;
     }

    public async Task<PaginatedResponseDto<SearchCustomerResponseDto>> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
     {
        
        var specification = new SearchCustomerSpecification(request.Skip, request.Take);
        var customers = await customerReadRepository.FindWithSpecification(specification).ToListAsync(cancellationToken);
        
        specification.SetIgnorePagination(true);
        var totalCount = await customerReadRepository.FindWithSpecification(specification).CountAsync(cancellationToken);

        var customerDtos = customers.Select(c => new SearchCustomerResponseDto(
            c.Id, 
            c.CustomerId, 
            c.FirstName, 
            c.LastName, 
            c.StoreName, 
            c.MobileNumber?.ToString(),
            c.Description
        )).ToList();

        return new(request.Skip, request.Take, totalCount, customerDtos);
        
     }

}
