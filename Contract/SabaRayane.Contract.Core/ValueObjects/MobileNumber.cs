using System.Text.RegularExpressions;
using Taran.Shared.Core.Exceptions;

namespace SabaRayane.Contract.Core.ValueObjects;

public sealed record MobileNumber
{
    private static readonly Regex MobileRegex = new(@"^09\d{9}$", RegexOptions.Compiled);

    public string Value { get; }

    private MobileNumber(string value) => Value = value;

    public static MobileNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainArgumentNullOrEmptyException(nameof(MobileNumber));

        value = value.Trim();

        if (!MobileRegex.IsMatch(value))
            throw new DomainInvalidArgumentException(nameof(MobileNumber));

        return new MobileNumber(value);
    }

    public override string ToString() => Value;
}
