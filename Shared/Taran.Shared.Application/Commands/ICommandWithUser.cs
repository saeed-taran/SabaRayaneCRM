using MediatR;

namespace Taran.Shared.Application.Commands;

public interface ICommandWithUser<ReturnType> : IRequest<ReturnType>
{
    void SetUserId(int userId);
}