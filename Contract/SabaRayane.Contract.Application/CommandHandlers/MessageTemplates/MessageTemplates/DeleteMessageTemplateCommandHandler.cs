using MediatR;
using SabaRayane.Contract.Application.Commands.s.MessageTemplates;
using SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;
using Taran.Shared.Core.Exceptions;
using Taran.Shared.Core.Repository;

namespace SabaRayane.Contract.Application.CommandHandlers.s.MessageTemplates;
public class DeleteMessageTemplateCommandHandler : IRequestHandler<DeleteMessageTemplateCommand, bool>
{
    private readonly IGenericWriteRepository<MessageTemplate, int> messageTemplateWriteRepository;
    private readonly IGenericReadRepository<MessageTemplate, int> messageTemplateReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteMessageTemplateCommandHandler(IGenericWriteRepository<MessageTemplate, int> messageTemplateWriteRepository, IGenericReadRepository<MessageTemplate, int> messageTemplateReadRepository, IUnitOfWork unitOfWork)
    {
        this.messageTemplateWriteRepository = messageTemplateWriteRepository;
        this.messageTemplateReadRepository = messageTemplateReadRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteMessageTemplateCommand request, CancellationToken cancellationToken)
    {
        var messageTemplate = await messageTemplateWriteRepository.GetByIdAsync(request.Id) ?? throw new DomainEntityNotFoundException(nameof(MessageTemplate));
        messageTemplateWriteRepository.Delete(messageTemplate);

        await unitOfWork.SaveChangesAsync();
        return true;
    }
}