using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taran.Shared.Language;

public record CultureConfiguration
{
    public string Language { get; init; }
    public string DateTime { get; init; }
}
