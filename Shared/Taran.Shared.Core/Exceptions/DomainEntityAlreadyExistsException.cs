using Taran.Shared.Core.Exceptions;

namespace Taran.Shared.Core.Exceptions;

public class DomainEntityAlreadyExistsException : BaseDomainException
{
    public DomainEntityAlreadyExistsException() : base("EntityAlreadyExists")
    {
    }
}
