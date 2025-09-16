using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Commands.Customers.Agreements;
using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using SabaRayane.Contract.Core.Specifications.s.Customers;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;
using Taran.Shared.Dtos.Services.Calendar;

namespace SabaRayane.Contract.Application.CommandHandlers.Customers.Agreements;
public class UpdateAgreementCommandHandler : IRequestHandler<UpdateAgreementCommand, bool>
{
    private readonly IGenericWriteRepository<Customer, int> customerWriteRepository;
    private readonly IGenericReadRepository<Agreement, int> agreementReadRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ICalendar calendar;

    public UpdateAgreementCommandHandler(IGenericWriteRepository<Customer, int> customerWriteRepository, IGenericReadRepository<Agreement, int> agreementReadRepository, IUnitOfWork unitOfWork, ICalendar calendar)
    {
        this.customerWriteRepository = customerWriteRepository;
        this.agreementReadRepository = agreementReadRepository;
        this.unitOfWork = unitOfWork;
        this.calendar = calendar;
    }

    public async Task<bool> Handle(UpdateAgreementCommand request, CancellationToken cancellationToken)
    {
        var loadedCustomer = await customerWriteRepository.FindWithSpecification(
            new LoadCustomerByIdSpecification(request.CustomerId)).FirstOrDefaultAsync(cancellationToken)
        ?? throw new DomainEntityNotFoundException(nameof(Customer));

        loadedCustomer.UpdateAgreement(request.GetUserId(), request.Id, request.ProductId, calendar.ConvertToDateOnly(request.AgreementDate), request.DurationInMonths, request.ExtraUsersCount);

        await unitOfWork.SaveChangesAsync();
        return true;
    }
}