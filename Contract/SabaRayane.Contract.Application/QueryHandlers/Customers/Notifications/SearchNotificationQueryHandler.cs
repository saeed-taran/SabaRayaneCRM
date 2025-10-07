using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Queries.Customers.CustomerAggregate;
using SabaRayane.Contract.Core.Specifications.Customers.CustomerAggregate;
using SabaRayane.Contract.Dtos.Customers.CustomerAggregate;
using Taran.Shared.Core.Repository;
using Taran.Shared.Dtos;
using Taran.Shared.Dtos.Services.Calendar;

namespace SabaRayane.Contract.Application.QueryHandlers.Customers.CustomerAggregate;

public class SearchNotificationQueryHandler : IRequestHandler<SearchNotificationQuery, PaginatedResponseDto<SearchNotificationResponseDto>>
{
    private readonly IGenericReadRepository<Core.Aggregates.CustomerAggregate.Notification, long> notificationReadRepository;
    private readonly ICalendar calendar;

    public SearchNotificationQueryHandler(IGenericReadRepository<Core.Aggregates.CustomerAggregate.Notification, long> notificationReadRepository, ICalendar calendar)
    {
        this.notificationReadRepository = notificationReadRepository;
        this.calendar = calendar;
    }

    public async Task<PaginatedResponseDto<SearchNotificationResponseDto>> Handle(SearchNotificationQuery request, CancellationToken cancellationToken)
    {

        var specification = new SearchNotificationSpecification(request.Skip, request.Take);
        var notifications = await notificationReadRepository.FindWithSpecification(specification).ToListAsync(cancellationToken);

        specification.SetIgnorePagination(true);
        var totalCount = await notificationReadRepository.FindWithSpecification(specification).CountAsync(cancellationToken);

        var notificationDtos = notifications.Select(c => new SearchNotificationResponseDto(
            c.Id,
            c.AgreementId,
            c.MessageTemplateId,
            c.MessageTemplate.Name,
            c.Message,
            c.NotificationStatus.ToString(),
            c.LastTryDate is not null ? calendar.ConvertDate(c.LastTryDate.Value) : "-",
            c.TryCount,
            c.ErrorDescription
        )).ToList();

        return new(request.Skip, request.Take, totalCount, notificationDtos);

    }

}