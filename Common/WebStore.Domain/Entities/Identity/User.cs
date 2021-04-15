using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public const string _administrator = "Admin";

        public const string _defaultAdminPassword = "AdPAss";
    }
}
