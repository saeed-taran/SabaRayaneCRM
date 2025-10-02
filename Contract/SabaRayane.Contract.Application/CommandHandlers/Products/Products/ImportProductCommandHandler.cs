using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Commands.Products.Products;
using SabaRayane.Contract.Core.Aggregates.ProductAggregate;
using SabaRayane.Contract.Core.Specifications.Products.Products;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;

namespace SabaRayane.Contract.Application.CommandHandlers.Products.Products;

public class ImportProductCommandHandler : IRequestHandler<ImportProductCommand, bool>
{
    private readonly IGenericWriteRepository<Product, int> productWriteRepository;
    private readonly IGenericReadRepository<Product, int> productReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public ImportProductCommandHandler(IGenericWriteRepository<Product, int> productWriteRepository, IUnitOfWork unitOfWork, IGenericReadRepository<Product, int> productReadRepository)
    {
        this.productWriteRepository = productWriteRepository;
        this.unitOfWork = unitOfWork;
        this.productReadRepository = productReadRepository;
    }

    public async Task<bool> Handle(ImportProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await productReadRepository.FindWithSpecification(new ExistanceCheckProductSpecification(request.Name))
            .FirstOrDefaultAsync(cancellationToken);
        if (existingProduct is not null)
            throw new DomainEntityAlreadyExistsException();

        var newProduct = new Product(request.GetUserId(), request.Name, request.Price, request.Description);
        await productWriteRepository.CreateAsync(newProduct);

        await unitOfWork.SaveChangesAsync();
        return true;
    }
}