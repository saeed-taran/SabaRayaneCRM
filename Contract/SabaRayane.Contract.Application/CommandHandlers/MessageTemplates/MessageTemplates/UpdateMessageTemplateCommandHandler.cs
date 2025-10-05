using MediatR;
using Microsoft.EntityFrameworkCore;
using SabaRayane.Contract.Application.Commands.s.MessageTemplates;
using SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;
using SabaRayane.Contract.Core.Specifications.MessageTemplates.MessageTemplates;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;

namespace SabaRayane.Contract.Application.CommandHandlers.s.MessageTemplates;
public class UpdateMessageTemplateCommandHandler : IRequestHandler<UpdateMessageTemplateCommand, bool>
{
    private readonly IGenericWriteRepository<MessageTemplate, int> messageTemplateWriteRepository;
    private readonly IGenericReadRepository<MessageTemplate, int> messageTemplateReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateMessageTemplateCommandHandler(IGenericWriteRepository<MessageTemplate, int> messageTemplateWriteRepository, IGenericReadRepository<MessageTemplate, int> messageTemplateReadRepository, IUnitOfWork unitOfWork)
    {
        this.messageTemplateWriteRepository = messageTemplateWriteRepository;
        this.messageTemplateReadRepository = messageTemplateReadRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateMessageTemplateCommand request, CancellationToken cancellationToken)
    {
        var existingMessageTemplate = await messageTemplateReadRepository.FindWithSpecification(
            new ExistanceCheckMessageTemplateSpecification(request.DaysUntilAgreementExpire)
        ).FirstOrDefaultAsync(m => m.Id != request.Id, cancellationToken);

        if (existingMessageTemplate is not null)
            throw new DomainEntityAlreadyExistsException();

        var messageTemplate = await messageTemplateWriteRepository.GetByIdAsync(request.Id) ?? throw new DomainEntityNotFoundException(nameof(MessageTemplate));
        messageTemplate.Update(request.GetUserId(), request.Name, request.Message, request.DaysUntilAgreementExpire);

        await unitOfWork.SaveChangesAsync();
        return true;
    }
}