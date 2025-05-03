using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.IdentityExceptions
{
    public sealed class AddressNotFoundException(string UserName) :NotFoundException($"Address with User Name {UserName} is not Found")
    {
    }
}
