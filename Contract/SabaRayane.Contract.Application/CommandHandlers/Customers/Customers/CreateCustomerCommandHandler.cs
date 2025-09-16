using MediatR;
using SabaRayane.Contract.Application.Commands.s.Customers;
using Taran.Shared.Core.Repository;
using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using SabaRayane.Contract.Core.ValueObjects;

namespace SabaRayane.Contract.Application.CommandHandlers.s.Customers;
public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, bool>
{
    private readonly IGenericWriteRepository<Customer, int> customerWriteRepository;
    private readonly IGenericReadRepository<Customer, int> customerReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateCustomerCommandHandler (IGenericWriteRepository<Customer, int> customerWriteRepository, IGenericReadRepository<Customer, int> customerReadRepository, IUnitOfWork unitOfWork)
     {
      this.customerWriteRepository = customerWriteRepository;
      this.customerReadRepository = customerReadRepository;
      this.unitOfWork = unitOfWork;
     }

   public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
     {
        MobileNumber? mobileNumber = string.IsNullOrWhiteSpace(request.MobileNumber) ? null : MobileNumber.Create(request.MobileNumber);
        
        var newCustomer = new Customer(request.GetUserId(), request.CustomerId, request.FirstName, request.LastName, request.StoreName, mobileNumber, request.Description);
        await customerWriteRepository.CreateAsync(newCustomer);

        await unitOfWork.SaveChangesAsync();
        return true;
     }
}
