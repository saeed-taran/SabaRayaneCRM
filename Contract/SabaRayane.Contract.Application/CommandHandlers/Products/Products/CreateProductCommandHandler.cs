using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Commands.s.Products;
using SabaRayane.Contract.Core.Aggregates.ProductAggregate;
using SabaRayane.Contract.Core.Specifications.Products.Products;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;

namespace SabaRayane.Contract.Application.CommandHandlers.s.Products;
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
{
    private readonly IGenericWriteRepository<Product, int> productWriteRepository;
    private readonly IGenericReadRepository<Product, int> productReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateProductCommandHandler(IGenericWriteRepository<Product, int> productWriteRepository, IGenericReadRepository<Product, int> productReadRepository, IUnitOfWork unitOfWork)
    {
        this.productWriteRepository = productWriteRepository;
        this.productReadRepository = productReadRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await productReadRepository.FindWithSpecification(new ExistanceCheckProductSpecification(request.Name)).FirstOrDefaultAsync(cancellationToken);
        if (existingProduct is not null)
            throw new DomainEntityAlreadyExistsException();

        var newProduct = new Product(request.GetUserId(), request.Name, request.Price, request.Description);
        await productWriteRepository.CreateAsync(newProduct);

        await unitOfWork.SaveChangesAsync();
        return true;
    }
}