using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.IdentityExceptions
{
    public sealed class UnauthorizedException(string message="Email Or Password is not valid"):Exception(message)
    {
    }
}
