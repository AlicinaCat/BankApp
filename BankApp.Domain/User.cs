using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Domain
{
    public class User : IdentityUser
    {
        public User ToList()
        {
            throw new NotImplementedException();
        }
    }
}
