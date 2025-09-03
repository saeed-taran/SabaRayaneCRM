using Taran.Shared.Core.Exceptions;

namespace Taran.Shared.Core.Exceptions;

public class DomainEntityNotFoundException : BaseDomainException
{
    public DomainEntityNotFoundException(string message) : base(message)
    {
    }
}
