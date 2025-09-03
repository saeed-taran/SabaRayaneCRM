namespace Taran.Shared.Core.Exceptions;

public class DomainEntityIsInUseException : BaseDomainException
{
    public DomainEntityIsInUseException() : base("EntityIsInUseAndCantBeDeleted")
    {
    }
}
