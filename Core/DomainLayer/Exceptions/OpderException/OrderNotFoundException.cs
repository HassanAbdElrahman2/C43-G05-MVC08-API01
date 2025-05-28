using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.OpderException
{
    public sealed class OrderNotFoundException(Guid Id):NotFoundException($"Order With Id {Id}Is Not Found")
    {
    }
}
