namespace Taran.Shared.Api.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class CustomeAuthorizeAttribute : Attribute
{
    public int AccessCode { get; init; }

    public CustomeAuthorizeAttribute(int accessCode)
    {
        AccessCode = accessCode;
    }
}
