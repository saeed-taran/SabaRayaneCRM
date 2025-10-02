using Taran.Shared.Application.Commands;
using Taran.Shared.Dtos;

namespace SabaRayane.Contract.Application.Commands.s.MessageTemplates;

public record DeleteMessageTemplateCommand : CommonDeleteRequestDto, ICommandWithUser<bool>
{
}
