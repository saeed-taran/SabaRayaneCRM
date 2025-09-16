using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Commands.Customers.Agreements;
using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using SabaRayane.Contract.Core.Specifications.s.Customers;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;

namespace SabaRayane.Contract.Application.CommandHandlers.Customers.Agreements;
public class DeleteAgreementCommandHandler : IRequestHandler<DeleteAgreementCommand, bool>
{
    private readonly IGenericWriteRepository<Customer, int> customerWriteRepository;
    private readonly IGenericReadRepository<Agreement, int> agreementReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteAgreementCommandHandler(IGenericWriteRepository<Customer, int> customerWriteRepository, IGenericReadRepository<Agreement, int> agreementReadRepository, IUnitOfWork unitOfWork)
    {
        this.customerWriteRepository = customerWriteRepository;
        this.agreementReadRepository = agreementReadRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteAgreementCommand request, CancellationToken cancellationToken)
    {
        var agreement = await agreementReadRepository.GetByIdAsync(request.Id);
        if (agreement is null)
            throw new DomainEntityNotFoundException(nameof(Agreement));
        var loadedCustomer = await customerWriteRepository.FindWithSpecification(
            new LoadCustomerByIdSpecification(agreement.CustomerId)).FirstOrDefaultAsync(cancellationToken)
        ?? throw new DomainEntityNotFoundException(nameof(Customer));

        loadedCustomer.DeleteAgreement(request.Id);

        await unitOfWork.SaveChangesAsync();
        return true;
    }
}