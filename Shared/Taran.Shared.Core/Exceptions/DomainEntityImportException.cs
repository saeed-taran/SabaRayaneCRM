using Taran.Shared.Core.Exceptions;

namespace Taran.Shared.Core.Exceptions;

public class DomainEntityImportException : BaseDomainException
{
    public string FieldName { get; private set; }

    public DomainEntityImportException(string fieldName, string message) : base(message)
    {
        FieldName = fieldName;
    }
}
