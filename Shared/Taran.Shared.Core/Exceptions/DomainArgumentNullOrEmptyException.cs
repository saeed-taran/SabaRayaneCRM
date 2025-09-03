namespace Taran.Shared.Core.Exceptions;

public class DomainArgumentNullOrEmptyException : BaseDomainException
{
    public DomainArgumentNullOrEmptyException() : base() { }

    public DomainArgumentNullOrEmptyException(string message) : base(message)
    {
    }

    public static string Ensure(string? value, string message)
    {
        return string.IsNullOrWhiteSpace(value) ? throw new DomainArgumentNullOrEmptyException(message) : value;
    }
}