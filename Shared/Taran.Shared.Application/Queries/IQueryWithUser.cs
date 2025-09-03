using MediatR;

namespace Taran.Shared.Application.Queries;

public interface IQueryWithUser<ReturnType> : IRequest<ReturnType>
{
    void SetUserId(int userId);
}