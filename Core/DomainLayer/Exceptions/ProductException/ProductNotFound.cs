﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.ProductException
{
    public sealed class ProductNotFound(int id) : NotFoundException($"Product With Id {id} Is Not Found")
    {
    }
}
