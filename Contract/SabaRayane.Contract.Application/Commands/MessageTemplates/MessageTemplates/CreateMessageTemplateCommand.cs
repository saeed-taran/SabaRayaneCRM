using SabaRayane.Contract.Dtos.s.MessageTemplates;
using Taran.Shared.Application.Commands;

namespace SabaRayane.Contract.Application.Commands.s.MessageTemplates;

public record CreateMessageTemplateCommand : CreateMessageTemplateRequestDto, ICommandWithUser<bool>
{
}
