using MediatR;

namespace Taran.Shared.Application.Queries;

public interface IQueryWithoutUser<ReturnType> : IRequest<ReturnType>
{
}