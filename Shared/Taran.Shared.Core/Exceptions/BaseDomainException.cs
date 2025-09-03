using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taran.Shared.Core.Exceptions;

public abstract class BaseDomainException : Exception
{
    public BaseDomainException() : base() { }

    public BaseDomainException(string message) : base(message)
    {
    }
}