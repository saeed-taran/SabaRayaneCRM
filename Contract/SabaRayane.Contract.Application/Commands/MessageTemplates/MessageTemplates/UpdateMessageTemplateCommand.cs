using SabaRayane.Contract.Dtos.s.MessageTemplates;
using Taran.Shared.Application.Commands;

namespace SabaRayane.Contract.Application.Commands.s.MessageTemplates;

public record UpdateMessageTemplateCommand : UpdateMessageTemplateRequestDto, ICommandWithUser<bool>
{
}
