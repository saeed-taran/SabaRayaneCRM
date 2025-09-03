using MediatR;

namespace Taran.Shared.Application.Queries;

public interface IQueryInternal<ReturnType> : IRequest<ReturnType>
{
}
