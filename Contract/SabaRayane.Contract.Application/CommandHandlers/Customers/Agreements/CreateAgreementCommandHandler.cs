using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Commands.Customers.Agreements;
using Taran.Shared.Core.Repository;
using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using SabaRayane.Contract.Core.Specifications.s.Customers;
using Taran.Shared.Core.Exceptions;
using SabaRayane.Contract.Core.Aggregates.ProductAggregate;

namespace SabaRayane.Contract.Application.CommandHandlers.Customers.Agreements;
public class CreateAgreementCommandHandler : IRequestHandler<CreateAgreementCommand, bool>
{
    private readonly IGenericWriteRepository<Customer, int> customerWriteRepository;
    private readonly IGenericReadRepository<Agreement, int> agreementReadRepository;
    private readonly IGenericReadRepository<Product, int> productReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateAgreementCommandHandler(IGenericWriteRepository<Customer, int> customerWriteRepository, IGenericReadRepository<Agreement, int> agreementReadRepository, IUnitOfWork unitOfWork, IGenericReadRepository<Product, int> productReadRepository)
    {
        this.customerWriteRepository = customerWriteRepository;
        this.agreementReadRepository = agreementReadRepository;
        this.unitOfWork = unitOfWork;
        this.productReadRepository = productReadRepository;
    }

    public async Task<bool> Handle(CreateAgreementCommand request, CancellationToken cancellationToken)
     {
        var loadedCustomer = await customerWriteRepository.FindWithSpecification(
            new LoadCustomerByIdSpecification(request.CustomerId)).FirstOrDefaultAsync(cancellationToken)
        ?? throw new DomainEntityNotFoundException(nameof(Customer));

        var product = await productReadRepository.GetByIdAsync(request.ProductId)
            ?? throw new DomainEntityNotFoundException(nameof(Product));

        loadedCustomer.AddAgreement(request.GetUserId(), request.ProductId, product.Price, DateOnly.Parse(request.AgreementDate), request.DurationInMonths, request.ExtraUsersCount);

        await unitOfWork.SaveChangesAsync();
        return true;
     }
}
