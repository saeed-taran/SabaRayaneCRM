using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Commands.Customers.Notifications;
using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;
using SabaRayane.Contract.Core.Specifications.Customers.Agreements;
using SabaRayane.Contract.Core.Specifications.s.Customers;
using SabaRayane.Contract.Core.Specifications.s.MessageTemplates;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;

namespace SabaRayane.Contract.Application.CommandHandlers.Customers.Notifications;

public class CreatePossibleNotificationsCommandHandler : IRequestHandler<CreatePossibleNotificationsCommand, bool>
{
    private readonly IGenericReadRepository<Agreement, int> agreementReadRepository;
    private readonly IGenericReadRepository<MessageTemplate, int> messageTemplateReadRepository;
    private readonly IGenericWriteRepository<Customer, int> customerWriteRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreatePossibleNotificationsCommandHandler(IGenericReadRepository<Agreement, int> agreementReadRepository, IGenericReadRepository<MessageTemplate, int> messageTemplateReadRepository, IGenericWriteRepository<Customer, int> customerWriteRepository, IUnitOfWork unitOfWork)
    {
        this.agreementReadRepository = agreementReadRepository;
        this.messageTemplateReadRepository = messageTemplateReadRepository;
        this.customerWriteRepository = customerWriteRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CreatePossibleNotificationsCommand request, CancellationToken cancellationToken)
    {
        var messageTemplates = await messageTemplateReadRepository.FindWithSpecification(
                        new SearchMessageTemplateSpecification(0, 100)
                    ).ToListAsync(cancellationToken);

        foreach (var messageTemplate in messageTemplates)
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            var agreementAboutToExpire = await agreementReadRepository.FindWithSpecification(
                new GetAgreementsAboutToExpireSpecification(0, 100, messageTemplate.DaysUntilAgreementExpire, messageTemplate.Id)
            ).ToListAsync(cancellationToken);

            if (!agreementAboutToExpire.Any()) continue;

            foreach (var agreement in agreementAboutToExpire)
            {
                var customer = await customerWriteRepository.FindWithSpecification(
                    new LoadCustomerByIdSpecification(agreement.CustomerId))
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new DomainEntityNotFoundException("Customer not found in background service!!");

                var template = messageTemplate.FillPlaceHolders(customer);
                customer!.AddNotification(customer.CreatorUserId, agreement.Id, messageTemplate.Id, template);

                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        return true;
    }
}
