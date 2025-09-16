using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Commands.s.Customers;
using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using SabaRayane.Contract.Core.Specifications.s.Customers;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;

namespace SabaRayane.Contract.Application.CommandHandlers.s.Customers;
public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly IGenericWriteRepository<Customer, int> customerWriteRepository;
    private readonly IGenericReadRepository<Customer, int> customerReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteCustomerCommandHandler (IGenericWriteRepository<Customer, int> customerWriteRepository, IGenericReadRepository<Customer, int> customerReadRepository, IUnitOfWork unitOfWork)
     {
      this.customerWriteRepository = customerWriteRepository;
      this.customerReadRepository = customerReadRepository;
      this.unitOfWork = unitOfWork;
     }

   public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
     {
        var customer = await customerWriteRepository.GetByIdAsync(request.Id) ?? throw new DomainEntityNotFoundException(nameof(Customer));
        customerWriteRepository.Delete(customer);

        await unitOfWork.SaveChangesAsync();
        return true;
     }
}
