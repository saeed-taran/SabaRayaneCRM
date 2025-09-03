using MediatR;

namespace Taran.Shared.Application.Commands;

public interface ICommandWithoutUser<ReturnType> : IRequest<ReturnType>
{
}