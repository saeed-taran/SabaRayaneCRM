using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Commands.s.Customers;
using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using SabaRayane.Contract.Core.ValueObjects;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;

namespace SabaRayane.Contract.Application.CommandHandlers.s.Customers;
public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
{
    private readonly IGenericWriteRepository<Customer, int> customerWriteRepository;
    private readonly IGenericReadRepository<Customer, int> customerReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateCustomerCommandHandler (IGenericWriteRepository<Customer, int> customerWriteRepository, IGenericReadRepository<Customer, int> customerReadRepository, IUnitOfWork unitOfWork)
     {
      this.customerWriteRepository = customerWriteRepository;
      this.customerReadRepository = customerReadRepository;
      this.unitOfWork = unitOfWork;
     }

   public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
     {
        var customer = await customerWriteRepository.GetByIdAsync(request.Id) ?? throw new DomainEntityNotFoundException(nameof(Customer));

        MobileNumber? mobileNumber = string.IsNullOrWhiteSpace(request.MobileNumber) ? null : MobileNumber.Create(request.MobileNumber);

        customer.Update(request.GetUserId(), request.CustomerId, request.FirstName, request.LastName, request.StoreName, mobileNumber, request.Description);

        await unitOfWork.SaveChangesAsync();
        return true;
     }
}
