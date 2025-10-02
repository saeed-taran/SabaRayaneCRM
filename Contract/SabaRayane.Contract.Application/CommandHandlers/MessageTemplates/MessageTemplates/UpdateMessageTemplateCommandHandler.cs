using MediatR;
using SabaRayane.Contract.Application.Commands.s.MessageTemplates;
using Taran.Shared.Core.Repository;
using SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;
using Taran.Shared.Core.Exceptions;

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
        var messageTemplate = await messageTemplateWriteRepository.GetByIdAsync(request.Id) ?? throw new DomainEntityNotFoundException(nameof(MessageTemplate));
        messageTemplate.Update(request.GetUserId(), request.Name, request.Message, request.DaysUntilAgreementExpire);

        await unitOfWork.SaveChangesAsync();
        return true;
    }
}