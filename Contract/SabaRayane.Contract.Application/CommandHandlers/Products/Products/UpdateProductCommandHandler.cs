using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Commands.s.Products;
using SabaRayane.Contract.Core.Aggregates.ProductAggregate;
using SabaRayane.Contract.Core.Specifications.Products.Products;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;

namespace SabaRayane.Contract.Application.CommandHandlers.s.Products;
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IGenericWriteRepository<Product, int> productWriteRepository;
    private readonly IGenericReadRepository<Product, int> productReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateProductCommandHandler(IGenericWriteRepository<Product, int> productWriteRepository, IGenericReadRepository<Product, int> productReadRepository, IUnitOfWork unitOfWork)
    {
        this.productWriteRepository = productWriteRepository;
        this.productReadRepository = productReadRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await productReadRepository.FindWithSpecification(new ExistanceCheckProductSpecification(request.Name))
            .FirstOrDefaultAsync(p => p.Id != request.Id, cancellationToken);
        if (existingProduct is not null)
            throw new DomainEntityAlreadyExistsException();

        var product = await productWriteRepository.GetByIdAsync(request.Id) ?? throw new DomainEntityNotFoundException(nameof(Product));
        product.Update(request.GetUserId(), request.Name, request.Price, request.Description);

        await unitOfWork.SaveChangesAsync();
        return true;
    }
}