﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WelcomeASP.Data
{
    public class User : IdentityUser
    {
        public String Tag { get; set; }
    }
}
