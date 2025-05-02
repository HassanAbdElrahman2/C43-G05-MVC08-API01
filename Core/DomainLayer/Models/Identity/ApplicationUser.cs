﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Identity
{
    public class ApplicationUser :IdentityUser
    {
        public string DispalayName { get; set; } = default!;
        public Address? Address { get; set; }
    }
}
