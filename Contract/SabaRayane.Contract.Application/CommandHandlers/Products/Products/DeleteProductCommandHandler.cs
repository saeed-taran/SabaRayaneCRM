using MediatR;
using SabaRayane.Contract.Application.Commands.s.Products;
using SabaRayane.Contract.Core.Aggregates.ProductAggregate;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;

namespace SabaRayane.Contract.Application.CommandHandlers.s.Products;
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IGenericWriteRepository<Product, int> productWriteRepository;
    private readonly IGenericReadRepository<Product, int> productReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteProductCommandHandler(IGenericWriteRepository<Product, int> productWriteRepository, IGenericReadRepository<Product, int> productReadRepository, IUnitOfWork unitOfWork)
    {
        this.productWriteRepository = productWriteRepository;
        this.productReadRepository = productReadRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productWriteRepository.GetByIdAsync(request.Id) ?? throw new DomainEntityNotFoundException(nameof(Product));
        productWriteRepository.Delete(product);

        await unitOfWork.SaveChangesAsync();
        return true;
    }
}