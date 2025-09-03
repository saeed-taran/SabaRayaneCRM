namespace Taran.Shared.Core.Exceptions;

public class DomainInvalidOperationException : BaseDomainException
{
    public DomainInvalidOperationException() : base() { }

    public DomainInvalidOperationException(string message) : base(message)
    {
    }
}