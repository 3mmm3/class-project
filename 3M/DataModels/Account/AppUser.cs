using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.DataModels
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
    }
}
