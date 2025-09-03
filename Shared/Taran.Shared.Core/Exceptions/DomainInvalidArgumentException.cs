namespace Taran.Shared.Core.Exceptions;

public class DomainInvalidArgumentException : BaseDomainException
{
    public DomainInvalidArgumentException() : base() { }

    public DomainInvalidArgumentException(string message) : base(message)
    {
    }
}