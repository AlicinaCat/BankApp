using BankApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.App.ViewModels
{
    public class UserWithRoles
    {
            public User User { get; set; }
            public bool IsAdmin { get; set; }
    }
}
