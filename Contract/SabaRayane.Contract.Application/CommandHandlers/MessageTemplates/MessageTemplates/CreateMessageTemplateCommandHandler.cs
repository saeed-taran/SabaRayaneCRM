using MediatR;
using SabaRayane.Contract.Application.Commands.s.MessageTemplates;
using SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;
using Taran.Shared.Core.Repository;

namespace SabaRayane.Contract.Application.CommandHandlers.s.MessageTemplates;
public class CreateMessageTemplateCommandHandler : IRequestHandler<CreateMessageTemplateCommand, bool>
{
    private readonly IGenericWriteRepository<MessageTemplate, int> messageTemplateWriteRepository;
    private readonly IGenericReadRepository<MessageTemplate, int> messageTemplateReadRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateMessageTemplateCommandHandler(IGenericWriteRepository<MessageTemplate, int> messageTemplateWriteRepository, IGenericReadRepository<MessageTemplate, int> messageTemplateReadRepository, IUnitOfWork unitOfWork)
    {
        this.messageTemplateWriteRepository = messageTemplateWriteRepository;
        this.messageTemplateReadRepository = messageTemplateReadRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CreateMessageTemplateCommand request, CancellationToken cancellationToken)
    {
        var newMessageTemplate = new MessageTemplate(request.GetUserId(), request.Name, request.Message, request.DaysUntilAgreementExpire);
        await messageTemplateWriteRepository.CreateAsync(newMessageTemplate);

        await unitOfWork.SaveChangesAsync();
        return true;
    }
}
